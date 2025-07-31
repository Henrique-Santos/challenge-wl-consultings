using Domain.Entities.Transfer;
using Domain.Entities.Wallet;

namespace Data;

public static class Seeder
{
    public static List<Wallet> GetWallets()
    {
        return new List<Wallet>
        {
            new("fd20f81f-6920-4f67-b42a-75dd48a811fe", 100),
            new("7b116278-ea36-473c-94d6-8179e624aa4f", 100),
        };
    }

    public static List<Transfer> GetTransfers()
    {
        return new List<Transfer>
        {
            new("fd20f81f-6920-4f67-b42a-75dd48a811fe", "7b116278-ea36-473c-94d6-8179e624aa4f", 50),
            new("7b116278-ea36-473c-94d6-8179e624aa4f", "fd20f81f-6920-4f67-b42a-75dd48a811fe", 30),
        };
    }
}
