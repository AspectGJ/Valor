// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("Hrtl9WVk8Q2rzDrOot7oA76HvBUiYS7A+ZkXTz0FEVUY+8fftSSaHLnn/v7jarzjoKz//uDuV67lJwuSi9a/f/4ln4rHyqCSx87ebB3+Ff4enZOcrB6dlp4enZ2cHbwRfa57hJINx0j7oQgwX1r4Yze4JSo5akCgHos105qoOe6LsPLgapbNqxTEMoXd+aIJ/OA6RMtCl/zntVPHh3opBgD/Qz1qEeQf18OJfp9vtF4OTffsG/Q9sgoqL7F1WPpn9YdgvPKYKHrF7jY3TdyfxYqzymMvJKbIWfYUECN8Nb3XTx+0+Efpx8yKg1CouAYx2QWg+SXtU1LT/8yCrjG3t1KmRjOsHp2+rJGalbYa1BprkZ2dnZmcn7678TXZGH3qg56fnZyd");
        private static int[] order = new int[] { 13,3,9,4,4,8,10,12,11,11,11,11,12,13,14 };
        private static int key = 156;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
