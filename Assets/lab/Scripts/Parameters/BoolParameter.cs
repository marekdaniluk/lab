using System;

namespace lab {
    /// <summary>
    /// Serializable bool parameter. This weird solution of inheriting from generic class is to avoid boiler code.
    /// </summary>
    [System.Serializable]
    public class BoolParameter : ASerializableParameter<string, bool> { }
}
