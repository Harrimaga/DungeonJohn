//using microsoft.xna.framework;
//using microsoft.xna.framework.graphics;
//using system;
//using system.collections.generic;
//using system.linq;
//using system.text;
//using system.threading.tasks;

//public class floor
//    {
//    room[,] floor;
//    int maxrooms = 20;
//    int minrooms = 15;

//    public floor()
//    {
//        floor = new room[9, 9];
//        //hele simpele layout voor testen
//        floor[5, 5] = new startroom(new vector2(5,5));
//        floor[6, 5] = new room();
//        floor[7, 5] = new endroom(new vector2(7,5));
//    }

//    void floorgenerator()
//    {
//        //todo
//    }

//    void nextfloor()
//    {
//        //todo dus new floor maken en oude weg halen
//    }
//    public virtual void draw(gametime gametime, spritebatch spritebatch)
//    {
//        foreach (room room in floor)
//        {
//            if (room != null)
//            {
//                room.draw(gametime, spritebatch);
//            }
//        }
//    }
//    public virtual void update(gametime gametime)
//    {
//        foreach (room room in floor)
//        {
//            if(room != null)
//            {
//                room.update(gametime);
//            }
//        }
//        //todo als nextfloor true is dan voor nextfloor() uit
//    }

//}

