using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Serializable float parameter.
    /// <para>This weird solution of inheriting from generic class is to avoid boiler code of serialization different types of parameters, because Unity does not serialize dictinaries.</para>
    /// </summary>
    [System.Serializable]
    public class FloatParameter : ASerializableParameter<string, float> {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FloatParameter() : base() { }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="floatParameter">Serializable parameter to copy</param>
        public FloatParameter(FloatParameter floatParameter) : base(floatParameter) {
            _keys = new List<string>(floatParameter._keys);
            _values = new List<float>(floatParameter._values);
        }
    }
}
