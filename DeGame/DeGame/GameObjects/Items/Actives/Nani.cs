using System;

public class Nani : Item, IActive
{
    public int maxCharges, charges;

    public Nani()
    {
        itemName = "nani";
        itemDescription = "Omae wa mou shindeiru!";
        Type = "active";
    }

    public int Charges
    {
        get
        {
            return charges;
        }

        set
        {
            charges = value;
        }
    }

    public int MaxCharges
    {
        get
        {
            return maxCharges;
        }

        set
        {
            maxCharges = value;
        }
    }

    public void ExecuteActive()
    {
        // Throw cutscene
        // Kill everyone in the room (DONT FORGET THE OMAEWAMOSHINDEIRUNANI!!!!
        throw new NotImplementedException();
    }
}

