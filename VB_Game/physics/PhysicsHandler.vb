Imports System.Text
Imports OpenTK
Imports OpenTK.Input
Imports VB_Game

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

    Private Shared scheduledDisposalObjs As New Queue(Of GameObject)

    Private Shared categoryBitMaskBodies As New Dictionary(Of Integer, List(Of RigidBody))

    Public Shared Sub init()
        For Each categoryBitmask In Constants.COLLISION_CATEGORIES
            categoryBitMaskBodies.Add(categoryBitmask, New List(Of RigidBody))
        Next
    End Sub

    ''' <summary>
    ''' Clears current physics bodies
    ''' </summary>
    Public Shared Sub clearBodies()
        physicsBodies.Clear()
        categoryBitMaskBodies.Clear()
        init()
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
            If physicsBodies(i).parent.affectedByGravity Then
                applyGravity(physicsBodies(i).parent)
            End If
        Next
        collisionsCheck()
        doDisposals()
    End Sub

    ''' <summary>
    ''' Disposes all items currently in queue
    ''' </summary>
    ''' <returns></returns>
    Private Shared Function doDisposals()
        While scheduledDisposalObjs.Count > 0
            removeBody(scheduledDisposalObjs.Dequeue())
        End While
    End Function

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
                        For j = 0 To categoryBitMaskBodies.Item(categoryBitmask).Count - 1
                            Dim body = categoryBitMaskBodies.Item(categoryBitmask)(j)
                            If Not body Is currentBody And PhysicUtils.doesCollide(currentBody, body) Then
                                handleCollision(currentBody, body)
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    ''' <summary>
    ''' Handles collision events, sending out events to colliders
    ''' </summary>
    ''' <param name="bodyA">Primary Body</param>
    ''' <param name="bodyB">Secondary Body</param>
    Private Shared Sub handleCollision(bodyA As RigidBody, bodyB As RigidBody)
        If bodyB.parent.GetType().IsAssignableFrom(GetType(Chest)) Then
            Debug.WriteLine("chest col")
        End If
        bodyA.parent.onCollide(bodyB.parent)
        bodyB.parent.onCollide(bodyA.parent)
    End Sub

    Public Shared Function getBodiesByCategory(categoryBitmask As Integer) As List(Of RigidBody)
        Return categoryBitMaskBodies.Item(categoryBitmask)
    End Function

    ''' <summary>
    ''' Schedules the obj to be disposed once collections are not being used
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Public Shared Sub scheduleDispose(obj As GameObject)
        scheduledDisposalObjs.Enqueue(obj)
    End Sub

    Private Shared Sub removeBody(obj As GameObject)
        For i = 0 To physicsBodies.Count - 1
            If physicsBodies(i).parent.Equals(obj) Then
                For Each keyValue As KeyValuePair(Of Integer, List(Of RigidBody)) In categoryBitMaskBodies
                    keyValue.Value.Remove(physicsBodies(i))
                Next
                physicsBodies.Remove(physicsBodies(i))
                Exit For
            End If
        Next
    End Sub

    Public Shared Function debugBodiesOut() As String
        Dim strBuilder As New StringBuilder()
        For i = 0 To physicsBodies.Count - 1
            Dim b = physicsBodies(i)
            strBuilder.AppendLine(b.parent.ToString() + ": " + b.parent.pos.ToString())
        Next
        Return strBuilder.ToString()
    End Function
End Class
