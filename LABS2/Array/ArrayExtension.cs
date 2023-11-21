namespace Labs2
{
    public static class ArrayExtension
    {
        public static Array DeleteNegativeElement(this Array array)
        {
            int counter = 0;
            for (int i = 0; i < array.Length; i++) if (array[i] >= 0) counter++;
            var newArray = new Array(counter);
            int indexNewElement = 0;
            for (int i = 0 ;i<array.Length;i++)
            {
                if (array[i] >= 0) newArray[indexNewElement++] = array[i];
            }
            return newArray;
        }
    }
}