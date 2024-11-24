using System;
public class Order
{
    public Recipe recipe;
    public int tableNumber;

    public Order(Recipe rec, int table)
    {
        recipe = rec;
        tableNumber = table;
    }
}