using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Serializable bool parameter.
    /// <para>This weird solution of inheriting from generic class is to avoid boiler code of serialization different types of parameters, because Unity does not serialize dictinaries.</para>
    /// </summary>
    [System.Serializable]
    public class BoolParameter : ASerializableParameter<string, bool> {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BoolParameter() : base() { }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="boolParameter">Serializable parameter to copy</param>
        public BoolParameter(BoolParameter boolParameter) : base(boolParameter) {
            _keys = new List<string>(boolParameter._keys);
            _values = new List<bool>(boolParameter._values);
        }
    }
}
