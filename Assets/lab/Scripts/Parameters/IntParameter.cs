namespace lab {
    /// <summary>
    /// Serializable int parameter. This weird solution of inheriting from generic class is to avoid boiler code.
    /// </summary>
    [System.Serializable]
    public class IntParameter : ASerializableParameter<string, int> { }
}
