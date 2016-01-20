using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq; // Redundant using directive
using System.Reflection;
using System.Text;
using JetBrains.Annotations;

namespace ReSharper.Showcase // Namespace doesn't correspond to file location
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

        [UsedImplicitly]
        Customer SetupCustomer(int id) // Missing private modifier
        {
            Customer customer = new Customer(); // Use var; use object initializer
            customer.Id = id;
            customer.Name = "Fanye East";
            return customer;
        }

        [UsedImplicitly]
        public bool IsReadOnly
        {
            // ReSharper disable once ConvertPropertyToExpressionBody
            get { return readOnly == null ? true : readOnly.Value; } // Merge conditional expression into conditional access
        }

        void Process(object item) // Method is recursive on all execution paths
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

        [UsedImplicitly]
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

            if (type == typeof(int)) // Possible unintended reference comparison
            {
                // Source: http://stackoverflow.com/questions/9234009/c-sharp-type-comparison-type-equals-vs-operator
                Type type = new TypeDelegator(typeof(int)); // Merge variables
                Console.WriteLine(type.Equals(typeof(int))); // Check for reference equality instead
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

            if (parameters != null) // 'if' statement can be rewritten as '?:' expression; Expression is always true
            {
                Console.WriteLine("Parameters are OK");
            }
            else
            {
                Console.WriteLine("Empty parameters"); // Code is heuristically unreachable
            }
        }
    }

    class SuspiciousCast
    {
        private readonly HashSet<IDataReader> readersToClose = new HashSet<IDataReader>(); // Content of collection is never updated

        [UsedImplicitly]
        protected void CheckReaders()
        {
            foreach (NHybridDataReader reader in readersToClose) // Suspicious cast
            {
            }
        }

        internal class NHybridDataReader // Member can be made private; Class is never instantiated
        {
        }
    }

    [UsedImplicitly]
    class InvalidCast
    {
        // Source: http://www.dotnetperls.com/invalidcastexception
        void Fail()
        {
            StringBuilder reference1 = new StringBuilder();
            object reference2 = reference1;
            StreamReader reference3 = (StreamReader) reference2; // Possible InvalidCastException
        }
    }

    public abstract class Abstraction
    {
        [UsedImplicitly]
        protected virtual void OnPreparedCommand() // Virtual method never overridden
        {
        }
    }

    [UsedImplicitly]
    internal class AbstractionImpl : Abstraction // Inconsistent modifier style; Class has virtual members but no inheritors
    {
        public virtual string ImplementationSpecific { get; set; }
    }

    class Customer
    {
        [UsedImplicitly] public string Name { get; set; }
        [UsedImplicitly] public int Id { get; set; } 
    }
}

// TODO Possible InvalidCastException when casting A to B in foreach loop: NHibernate, FilterKey.cs 
// TODO Field initializer value ignored during initialization: NHibernate, AbstractQueryImpl.cs
// TODO Possible multiple enumeration
// TODO Possible unintended reference comparison: NHibernate, AbstractQueryImpl.cs
// TODO Method return value is never used: NHibernate, AbstractQueryImpl.cs
// TODO Simplify LINQ expression: NHibernate, NH2459/Test.cs
// TODO Introduce optional parameters: NHibernate, SqlBaseBuilder.cs
// TODO Use String.IsNullOrEmpty: NHibernate, SqlBaseBuilder.cs
// TODO Possible ambiguity while accessing by this interface: NHibernate, ISetSnapshot.cs
// TODO Convert to auto-property with private setter: NHibernate, Join.cs
// TODO Loop can be converted into LINQ expression: NHibernate, Table.cs, foreach (Column column in ColumnIterator)
// TODO Convert to constant: NHibernate, IPersistentIdentifierGenerator.cs
// TODO Check for reference equality instead: NHibernate, AliasToBeanResultTransformer.cs
// TODO The given expression of 'is' operator is always of the provided type: NHibernate, QueryOverFixture.cs