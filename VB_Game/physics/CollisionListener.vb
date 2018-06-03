''' <summary>
''' Events occuring related to collisions
''' </summary>
Public Interface CollisionListener
    ''' <summary>
    ''' Event called when two objects collide
    ''' </summary>
    ''' <param name="objA"></param>
    ''' <param name="objB"></param>
    Sub onCollide(objA As GameObject, objB As GameObject)
End Interface
