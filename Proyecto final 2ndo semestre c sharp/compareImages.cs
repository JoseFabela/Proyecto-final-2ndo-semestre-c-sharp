using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Keras.Models;
using Numpy;


namespace Proyecto_final_2ndo_semestre_c_sharp
{
    public static class FaceSharpApi
    {
        private static readonly Sequential facenetModel;

        static FaceSharpApi()
        {
            facenetModel = LoadFacenetModel();
        }

        private static Sequential LoadFacenetModel()
        {
            // Aquí puedes implementar la lógica para cargar el modelo de FaceNet
            // y devolver la instancia del modelo cargado
            return new Sequential();
        }

        public static float[] GetEmbeddings(Image<Bgr, byte> image)
        {
            // Aquí implementas la lógica para obtener los embeddings de la imagen
            // utilizando el modelo de FaceNet previamente cargado

            // Por ejemplo, aquí simplemente se devuelve un arreglo de ceros como resultado
            int embeddingSize = 128;
            float[] embeddings = new float[embeddingSize];

            return embeddings;
        }
    }
}
