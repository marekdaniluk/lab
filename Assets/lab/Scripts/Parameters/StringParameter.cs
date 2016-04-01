namespace lab {
    /// <summary>
    /// Serializable string parameter. This weird solution of inheriting from generic class is to avoid boiler code.
    /// </summary>
    [System.Serializable]
    public class StringParameter : ASerializableParameter<string, string> { }
}
