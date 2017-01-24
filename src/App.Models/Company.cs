namespace App.Models
{
    //Don't like bleeding my objects across domains but it's better than having cyclic dependancies on App.Data and App
    //Would also have different properties coming out from the 
    public class Company
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        //Want some immutability of the object.
        public Company(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}