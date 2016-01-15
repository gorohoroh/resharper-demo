using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace ReSharper_Demo
{
    public class AssortedInspections
    {
        private string _alias; // Naming
        private int _scalarColumnIndex;

        public AssortedInspections(string alias)
        {
            _alias = alias;
        }

        [UsedImplicitly]
        public string Alias // Convert to auto-property
        {
            get { return _alias; } // Convert to expression body
        }

        void Process(object item)
        {
            if (item != null) // Use null propagation
            {
                item.ToString(); // Return value not used
            }

            Process(item); // Convert recursion to iteration
        }

        [UsedImplicitly]
        void GetSQL(SqlString sqlString) // Naming, with an option to add abbreviation
        {
            
        }

    }

    class SuspiciousCast
    {
        private readonly HashSet<IDataReader> readersToClose = new HashSet<IDataReader>();
        // Collection is never updated

        protected void CheckReaders()
        {
            foreach (NHybridDataReader reader in readersToClose)
            {
            }
        }

        internal class NHybridDataReader
        {
        }
    }

    class InvalidCast
    {
        // Source: http://www.dotnetperls.com/invalidcastexception
        void Fail()
        {
            StringBuilder reference1 = new StringBuilder();
            object reference2 = reference1;
            StreamReader reference3 = (StreamReader) reference2;
        }
    }

    class InvalidCast2
    {
        // TODO Possible InvalidCastException when casting A to B in foreach loop: NHibernate, FilterKey.cs 
    }

    public abstract class Abstraction
    {
        [UsedImplicitly]
        protected virtual void OnPreparedCommand() // Virtual method never overridden
        {
        }
    }

    class AbstractionImpl : Abstraction
    {
    }
}