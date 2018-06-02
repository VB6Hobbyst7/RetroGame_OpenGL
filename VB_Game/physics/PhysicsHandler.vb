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
    Public Shared physicsBodies As New List(Of Entity)

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
        Debug.WriteLine(e.velocity)
    End Sub

    ''' <summary>
    ''' Updates physic
    ''' </summary>
    ''' <param name="d">Time delta since last update</param>
    Public Shared Sub update(d As Double)
        delta = d
        'Apply gravity to physic bodies
        For i = 0 To physicsBodies.Count - 1
            applyGravity(physicsBodies(i))
        Next

        'Handle collisions
    End Sub


End Class
