using PartsTest.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ComponentService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IComponentService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IComponentService
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        string CreateComponentDTO(ComponentDTO ComponentDTO);

        [OperationContract]
        IOrderedEnumerable<ComponentDTO> GetAllComponents();

        [OperationContract]
        ComponentDTO GetComponentDTOById(string id);

        [OperationContract]
        void DeleteComponentDTO(string id);

        [OperationContract]
        void PutEmployeeInfo(string id, ComponentDTO component);
     
    }


    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
