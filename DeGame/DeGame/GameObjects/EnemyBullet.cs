using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

class EnemyBullet : Bullet
{
    public EnemyBullet(Vector2 Startposition, int Direction, int layer = 0, string id = "bullet") : base(Startposition, Direction, layer, id)
    {
    }
}

