Imports OpenTK

''' <summary>
''' Handles all physics related calculations and operations
''' </summary>
Public Class PhysicsHandler

    ''' <summary>
    ''' Time since last update
    ''' </summary>
    Private Shared delta As Double

    ''' <summary>
    ''' List of all entities affected by physics (e.g. gravity)
    ''' </summary>
    Private Shared physicsBodies As New List(Of RigidBody)

    Private Shared categoryBitMaskBodies As New Dictionary(Of Integer, List(Of RigidBody))

    Public Shared Sub init()
        For Each categoryBitmask In Constants.COLLISION_CATEGORIES
            categoryBitMaskBodies.Add(categoryBitmask, New List(Of RigidBody))
        Next
    End Sub

    ''' <summary>
    ''' Adds body to physics bodies and does setup for bitmasks
    ''' </summary>
    ''' <param name="rigidBody"></param>
    Public Shared Sub addPhysicsBody(rigidBody As RigidBody)
        physicsBodies.Add(rigidBody)
        categoryBitMaskBodies.Item(rigidBody.categoryBitMask).Add(rigidBody)
    End Sub

    ''' <summary>
    ''' Applies acceleration due to gravity (defined by constant) to entity
    ''' </summary>
    ''' <param name="e">Target entity</param>
    Public Shared Sub applyGravity(e As Entity)
        applyAcceleration(e, Constants.ACC_GRAVITY)
    End Sub

    ''' <summary>
    ''' Applies a constant acceleration to an entity
    ''' </summary>
    ''' <param name="e">Target Entity</param>
    ''' <param name="acc">Acceleration in ms-2</param>
    Public Shared Sub applyAcceleration(e As Entity, acc As Vector2)
        e.velocity += (PhysicUtils.metersToPixels(acc) * delta)
    End Sub

    ''' <summary>
    ''' Updates physic
    ''' </summary>
    ''' <param name="d">Time delta since last update</param>
    Public Shared Sub update(d As Double)
        delta = d
        'Apply gravity to physic bodies
        For i = 0 To physicsBodies.Count - 1
            'Checks if body is instance of entity
            'If physicsBodies(i).parent.GetType.IsAssignableFrom(GetType(Player)) Then
            '    applyGravity(physicsBodies(i).parent)
            'End If
        Next
        collisionsCheck()
    End Sub

    ''' <summary>
    ''' Checks collisions between all compatible bodies
    ''' </summary>
    Private Shared Sub collisionsCheck()
        For i = 0 To physicsBodies.Count - 1
            Dim currentBody = physicsBodies(i)
            If currentBody.collisionBitMask <> Constants.Physics_COLLISION.NO_COLLISION Then 'Object can collide with others
                For Each categoryBitmask In Constants.COLLISION_CATEGORIES
                    If PhysicUtils.canCollide(currentBody.collisionBitMask, categoryBitmask) Then
                        'Bitmasks can collide so check all corresponding bodies for collisions
                        For Each body In categoryBitMaskBodies.Item(categoryBitmask)
                            If PhysicUtils.doesCollide(currentBody, body) Then
                                handleCollision(currentBody, body)
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    ''' <summary>
    ''' Handles collision events, sending out events primary collider
    ''' </summary>
    ''' <param name="bodyA">Primary Body</param>
    ''' <param name="bodyB">Secondary Body</param>
    Private Shared Sub handleCollision(bodyA As RigidBody, bodyB As RigidBody)
        bodyA.parent.onCollide(bodyB.parent)
    End Sub

    Public Shared Function getBodiesByCategory(categoryBitmask As Integer) As List(Of RigidBody)
        Return categoryBitMaskBodies.Item(categoryBitmask)
    End Function


End Class
