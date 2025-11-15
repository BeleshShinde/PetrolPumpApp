using System;
using System.Collections.Generic;
using System.Linq;

namespace PetrolPumpApp.Models
{
    public static class InMemoryStorage
    {
        private static List<DispensingRecord> records = new List<DispensingRecord>();
        private static int nextId = 1;

        public static List<DispensingRecord> GetAll()
        {
            return records.OrderByDescending(r => r.CreatedAt).ToList();
        }

        public static DispensingRecord Add(DispensingRecord record)
        {
            record.Id = nextId++;
            record.CreatedAt = DateTime.Now;
            records.Add(record);
            return record;
        }

        public static DispensingRecord GetById(int id)
        {
            return records.FirstOrDefault(r => r.Id == id);
        }
    }
}