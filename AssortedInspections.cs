using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace ReSharper_Demo
{
    public class AssortedInspections
    {
        private string _alias; // Naming
        private int _scalarColumnIndex;
        [UsedImplicitly] private string comment;
        private bool? readOnly;

        public AssortedInspections(string alias, bool? readOnly)
        {
            _alias = alias;
            this.readOnly = readOnly;
        }

        [UsedImplicitly]
        internal protected string Comment => comment; // Inconsistent order of modifiers

        [UsedImplicitly]
        public string Alias // Convert to auto-property
        {
            get { return _alias; } // Convert to expression body
        }

        [UsedImplicitly]
        public AssortedInspections SetComment(string comment) // Parameter hides field
        {
            this.comment = comment;
            return this;
        }

        public bool IsReadOnly
        {
            // ReSharper disable once ConvertPropertyToExpressionBody
            get { return readOnly == null ? true : readOnly.Value; } // Merge conditional expression into conditional access
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

        private void GuessType(object param, IEnumerable vals, object type)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param", "Type can not be guessed for a null value."); // Use 'nameof' expression
            }

            if (type == null)
            {
                throw new ArgumentNullException(string.Format("Empty type '{0}'", type.GetType())); // Use string interpolation + possible NRE
            }

            if (vals == null)
            {
                throw new ArgumentNullException("vals"); // Use 'nameof' expression
            }

            bool serializable = (type != null && type is Abstraction); // Merge sequential checks

        }

        [UsedImplicitly]
        private static void ProcessParams(string[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++) // 'for' can be converted into 'foreach'
            {
                var namedParam = parameters[i];
                if (namedParam != null)
                {
                    break;
                }
            }
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

// TODO Possible InvalidCastException when casting A to B in foreach loop: NHibernate, FilterKey.cs 
// TODO Field initializer value ignored during initialization: NHibernate, AbstractQueryImpl.cs
// TODO Possible multiple enumeration
// TODO Possible unintended reference comparison: NHibernate, AbstractQueryImpl.cs