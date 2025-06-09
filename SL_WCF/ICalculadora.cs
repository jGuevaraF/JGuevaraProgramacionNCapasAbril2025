using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICalculadora" in both code and config file together.
    [ServiceContract]
    public interface ICalculadora
    {
        [OperationContract]
        void DoWork(); //firma de metodo = que tengo que hacer

        [OperationContract]
        int Suma(int numero1, int numero2); //metodo no implementado

        [OperationContract]
        int Resta(int numero1, int numero2); //metodo no implementado

        //int MyProperty { get; set; }


    }

    //[DataContract]
    //public class Ejemplo
    //{
    //    [DataMember]
    //    public int Id { get; set; }
    //}
}
