using Microsoft.Xna.Framework;

class Floor
{
    string[,] floorarray = new string[100, 100];

    public void MergeArrays (string[,] roomarray)
    {
        int a = 0, b = 0;
        // algoritme dat kamers indeeld en offset waarde aan a en b geeft
        for (int x = 0; x == 10; x++)
            for (int y = 0; y == 10; y++)
                floorarray[x + a, y + b] = roomarray[x, y];
    }

    public void Draw(GameTime gameTime)
    {
        for (int x = 0; x < 100; x++)
            for (int y = 0; y < 100; y++)
            {
                //switch (roomarray[x,y])
                //{
                //    case '?':
                //        return "Blank";
                //    case '.':
                //        return "Normal";
                //    case '!':
                //        return "Rock";
                //    case '+':
                //        return "Wall";
                //    case 'X':
                //        return "End";
                //    case 'S':
                //        return "Start";
                //    default:
                //        return "Unkown";
            }
    }


}

