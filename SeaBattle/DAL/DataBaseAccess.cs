using GameDBContext.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataBaseAccess
    {
        public static GameDbContext DbContext;
        public static void ConnectToDatabase()
        {
            DbContext = new GameDbContext();
        }
    }
}
