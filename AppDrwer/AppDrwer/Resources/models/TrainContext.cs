using System;
using System.Collections.Generic;
using System.Linq;
using Android.Database.Sqlite;
using Android.Util;
using SQLite;

namespace AppDrwer.Resources.model
{
    class TrainContext
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool CreateDataBase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Trains3.db")))
                {
                    connection.CreateTable<Train>();
                    for (int i = 0; i < 20; i++)
                    {
                        Train train = new Train
                        {
                            Punkt = "punkt" + (i + 1),
                            Number = (i + 1),
                            Date = new DateTime(2008, 2, 2, (i + 1), (50 - i), 0),
                            Ob = (i + 1),
                            Kupe = (i + 1),
                            Pl = (i + 1),
                            Lukc = (i + 1)
                        };
                        connection.Insert(train);
                    }

                }
                return true;
            }
            catch (Android.Database.Sqlite.SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        public List<Train> GetList()
        {
            try
            {
                List<Train> list = new List<Train>();
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Trains3.db")))
                {
                    return connection.Table<Train>().ToList();
                }

            }
            catch (SQLite.SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                CreateDataBase();
                return GetList();
            }
        }

    }
}