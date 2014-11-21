using Newtonsoft.Json;

namespace MassiveInteractiveGraph.Common
{
    public interface IJsonSerializer
    {
        string Serialize(object objectToSerialize);
        T Deserialize<T>(string toDeserialize);
    }

    /// <summary>
    /// http://james.newtonking.com/pages/json-net.aspx
    /// </summary>
    /// <see cref="http://james.newtonking.com/projects/json/help/"/>
    public class NewtonJsonSerializer : IJsonSerializer
    {
        public string Serialize(object toSerialize)
        {
            var toReturn = JsonConvert.SerializeObject(toSerialize);
            return toReturn;
        }

        public T Deserialize<T>(string toDeserialize)
        {
            var toReturn = JsonConvert.DeserializeObject<T>(toDeserialize);
            return toReturn;
        }
    }
}
