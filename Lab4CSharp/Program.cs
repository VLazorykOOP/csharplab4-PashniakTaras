using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("What task do you want?");
        Console.WriteLine("1. Task 1");
        Console.WriteLine("2. Task 2");
        Console.WriteLine("3. Task 3");
        Console.WriteLine("4. Exit");

        int choice;
        bool isValidChoice = false;

        do
        {
            Console.Write("Enter number of task: ");
            isValidChoice = int.TryParse(Console.ReadLine(), out choice);

            if (!isValidChoice || choice < 1 || choice > 4)
            {
                Console.WriteLine("This task does not exist");
                isValidChoice = false;
            }
        } while (!isValidChoice);

        switch (choice)
        {
            case 1:
                task1();
                break;
            case 2:
                task2();
                break;
            case 3:
                task3();
                break;
            case 4:
                break;
        }
    }

    class ATriangle
    {
        private double a, b;
        private string color;

        public ATriangle(double a, double b, string color)
        {
            this.a = a;
            this.b = b;
            this.color = color;
        }

        public object this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return a;
                    case 1:
                        return b;
                    case 2:
                        return color;
                    default:
                        throw new IndexOutOfRangeException($"Index {index} is out of range. Only indices 0, 1, and 2 are allowed.");
                }
            }
        }

        public static ATriangle operator ++(ATriangle triangle)
        {
            triangle.a++;
            triangle.b++;
            return triangle;
        }

        public static ATriangle operator --(ATriangle triangle)
        {
            triangle.a--;
            triangle.b--;
            return triangle;
        }

        public static bool operator true(ATriangle triangle)
        {
            return (triangle.a * triangle.a + triangle.b * triangle.b > 0);
        }

        public static bool operator false(ATriangle triangle)
        {
            return !(triangle.a * triangle.a + triangle.b * triangle.b > 0);
        }

        public static ATriangle operator +(ATriangle triangle, double scalar)
        {
            triangle.a += scalar;
            triangle.b += scalar;
            return triangle;
        }

        public static implicit operator string(ATriangle triangle)
        {
            return $"a: {triangle.a}; b: {triangle.b}; color: {triangle.color} ";
        }

        public static explicit operator ATriangle(string triangleString)
        {
            string[] parts = triangleString.Split(';');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid string format for ATriangle");
            }

            double a, b;
            string color;

            if (!double.TryParse(parts[0].Split(':')[1].Trim(), out a))
            {
                throw new ArgumentException("Invalid value for side 'a' in ATriangle");
            }

            if (!double.TryParse(parts[1].Split(':')[1].Trim(), out b))
            {
                throw new ArgumentException("Invalid value for side 'b' in ATriangle");
            }

            color = parts[2].Split(':')[1].Trim();

            return new ATriangle(a, b, color);
        }
    }

    static void task1()
    {
        ATriangle triangle = new ATriangle(3, 4, "Red");
        Console.WriteLine(triangle[0]);
        Console.WriteLine(triangle[1]);
        Console.WriteLine(triangle[2]);

        ATriangle Operator1 = ++triangle;
        Console.WriteLine("Operator++: " + Operator1);
        ATriangle Operator2 = --triangle;
        Console.WriteLine("Operator--: " + Operator2);

        ATriangle anotherTriangle = new ATriangle(2, 12, "Blue");

        if (triangle)
        {
            Console.WriteLine($"triangle {triangle} exists");
        }
        else
        {
            Console.WriteLine($"triangle {triangle} doesn't exist");
        }

        if (anotherTriangle)
        {
            Console.WriteLine($"triangle {anotherTriangle} exists");
        }
        else
        {
            Console.WriteLine($"triangle {anotherTriangle} doesn't exist");
        }

        double scalar = 2.5;
        ATriangle Operator3 = triangle + scalar;
        Console.WriteLine($"Operator+ ({scalar}): " + Operator3);
    }

    class VectorDouble
    {
        protected double[] FArray;
        protected uint num;
        protected int codeError;
        protected static uint num_vd;

        public VectorDouble()
        {
            FArray = new double[1];
            num = 1;
            codeError = 0;
            num_vd++;
        }

        public VectorDouble(uint size)
        {
            FArray = new double[size];
            num = size;
            codeError = 0;
            num_vd++;
        }

        public VectorDouble(uint size, double initValue)
        {
            FArray = new double[size];
            num = size;
            codeError = 0;
            num_vd++;

            for (int i = 0; i < size; i++)
            {
                FArray[i] = initValue;
            }
        }

        ~VectorDouble()
        {
            Console.WriteLine("VectorDouble has been destructed.");
        }

        public void InputElements()
        {
            for (int i = 0; i < FArray.Length; i++)
            {
                Console.Write($"Enter element {i + 1}: ");
                if (!double.TryParse(Console.ReadLine(), out FArray[i]))
                {
                    Console.WriteLine("Invalid input! Please enter a valid double.");
                    i--;
                }
            }
        }

        public void PrintElements()
        {
            Console.WriteLine("VectorDouble elements:");
            foreach (var element in FArray)
            {
                Console.WriteLine(element);
            }
        }

        public void AssignValue(double value)
        {
            for (int i = 0; i < FArray.Length; i++)
            {
                FArray[i] = value;
            }
        }

        public static uint CountVectors()
        {
            return num_vd;
        }

        public uint Size
        {
            get { return num; }
        }

        public int CodeError
        {
            get { return codeError; }
            set { codeError = value; }
        }

        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= FArray.Length)
                {
                    codeError = -1;
                    return 0;
                }
                else
                {
                    codeError = 0;
                    return FArray[index];
                }
            }
            set
            {
                if (index < 0 || index >= FArray.Length)
                {
                    codeError = -1;
                }
                else
                {
                    FArray[index] = value;
                    codeError = 0;
                }
            }
        }

        public static VectorDouble operator ++(VectorDouble vector)
        {
            for (int i = 0; i < vector.FArray.Length; i++)
            {
                vector.FArray[i]++;
            }
            return vector;
        }

        public static VectorDouble operator --(VectorDouble vector)
        {
            for (int i = 0; i < vector.FArray.Length; i++)
            {
                vector.FArray[i]--;
            }
            return vector;
        }

        public static bool operator true(VectorDouble vector)
        {
            if (vector.num != 0)
            {
                foreach (var element in vector.FArray)
                {
                    if (element != 0)
                        return true;
                }
            }
            return false;
        }

        public static bool operator false(VectorDouble vector)
        {
            return !vector;
        }

        public static bool operator !(VectorDouble vector)
        {
            return vector.num != 0;
        }

        public static VectorDouble operator ~(VectorDouble vector)
        {
            for (int i = 0; i < vector.FArray.Length; i++)
            {
                vector.FArray[i] = (double)~(int)vector.FArray[i];
            }
            return vector;
        }

        public static VectorDouble operator +(VectorDouble vector1, VectorDouble vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                Console.WriteLine("Vectors must have the same size to perform addition.");
                return null;
            }

            VectorDouble result = new VectorDouble(vector1.Size);

            for (int i = 0; i < result.Size; i++)
            {
                result[i] = vector1[i] + vector2[i];
            }

            return result;
        }

        public static VectorDouble operator +(VectorDouble vector, double scalar)
        {
            VectorDouble result = new VectorDouble(vector.Size);

            for (int i = 0; i < result.Size; i++)
            {
                result[i] = vector[i] + scalar;
            }

            return result;
        }

        public static VectorDouble operator -(VectorDouble vector1, VectorDouble vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                Console.WriteLine("Vectors must have the same size to perform subtraction.");
                return null;
            }

            VectorDouble result = new VectorDouble(vector1.Size);

            for (int i = 0; i < result.Size; i++)
            {
                result[i] = vector1[i] - vector2[i];
            }

            return result;
        }

        public static VectorDouble operator -(VectorDouble vector, double scalar)
        {
            VectorDouble result = new VectorDouble(vector.Size);

            for (int i = 0; i < result.Size; i++)
            {
                result[i] = vector[i] - scalar;
            }

            return result;
        }

        public static VectorDouble operator *(VectorDouble vector1, VectorDouble vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                Console.WriteLine("Vectors must have the same size to perform multiplication.");
                return null;
            }

            VectorDouble result = new VectorDouble(vector1.Size);

            for (int i = 0; i < result.Size; i++)
            {
                result[i] = vector1[i] * vector2[i];
            }

            return result;
        }

        public static VectorDouble operator *(VectorDouble vector, double scalar)
        {
            VectorDouble result = new VectorDouble(vector.Size);

            for (int i = 0; i < result.Size; i++)
            {
                result[i] = vector[i] * scalar;
            }

            return result;
        }

        public static VectorDouble operator /(VectorDouble vector1, VectorDouble vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                Console.WriteLine("Vectors must have the same size to perform division.");
                return null;
            }

            VectorDouble result = new VectorDouble(vector1.Size);

            for (int i = 0; i < result.Size; i++)
            {
                result[i] = vector1[i] / vector2[i];
            }

            return result;
        }

        public static VectorDouble operator /(VectorDouble vector, double scalar)
        {
            VectorDouble result = new VectorDouble(vector.Size);

            for (int i = 0; i < result.Size; i++)
            {
                result[i] = vector[i] / scalar;
            }

            return result;
        }

        public static VectorDouble operator %(VectorDouble vector1, VectorDouble vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                Console.WriteLine("Vectors must have the same size to perform modulo operation.");
                return null;
            }

            VectorDouble result = new VectorDouble(vector1.Size);

            for (int i = 0; i < result.Size; i++)
            {
                result[i] = vector1[i] % vector2[i];
            }

            return result;
        }

        public static VectorDouble operator %(VectorDouble vector, double scalar)
        {
            VectorDouble result = new VectorDouble(vector.Size);

            for (int i = 0; i < result.Size; i++)
            {
                result[i] = vector[i] % scalar;
            }

            return result;
        }

        public static VectorDouble operator |(VectorDouble vector1, VectorDouble vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                Console.WriteLine("Vectors must have the same size to perform bitwise OR.");
                return null;
            }

            VectorDouble result = new VectorDouble(vector1.Size);

            for (int i = 0; i < result.Size; i++)
            {
                result[i] = (double)((int)vector1[i] | (int)vector2[i]);
            }

            return result;
        }

        public static VectorDouble operator |(VectorDouble vector, double scalar)
        {
            VectorDouble result = new VectorDouble(vector.Size);
            for (int i = 0; i < result.Size; i++)
            {
                result[i] = (double)((int)vector[i] | (int)scalar);
            }
            return result;
        }

        public static VectorDouble operator ^(VectorDouble vector1, VectorDouble vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                Console.WriteLine("Vectors must have the same size to perform bitwise XOR.");
                return null;
            }

            VectorDouble result = new VectorDouble(vector1.Size);

            for (int i = 0; i < result.Size; i++)
            {
                result[i] = (double)((int)vector1[i] ^ (int)vector2[i]);
            }

            return result;
        }

        public static VectorDouble operator ^(VectorDouble vector, double scalar)
        {
            VectorDouble result = new VectorDouble(vector.Size);
            for (int i = 0; i < result.Size; i++)
            {
                result[i] = (double)((int)vector[i] ^ (int)scalar);
            }
            return result;
        }

        public static VectorDouble operator &(VectorDouble vector1, VectorDouble vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                Console.WriteLine("Vectors must have the same size to perform bitwise AND.");
                return null;
            }

            VectorDouble result = new VectorDouble(vector1.Size);

            for (int i = 0; i < result.Size; i++)
            {
                result[i] = (double)((int)vector1[i] & (int)vector2[i]);
            }

            return result;
        }

        public static VectorDouble operator &(VectorDouble vector, double scalar)
        {
            VectorDouble result = new VectorDouble(vector.Size);
            for (int i = 0; i < result.Size; i++)
            {
                result[i] = (double)((int)vector[i] & (int)scalar);
            }
            return result;
        }

        public static VectorDouble operator >>(VectorDouble vector, int shift)
        {
            VectorDouble result = new VectorDouble(vector.Size);

            for (int i = 0; i < result.Size; i++)
            {
                result[i] = (double)((int)vector[i] >> shift);
            }

            return result;
        }

        public static VectorDouble operator <<(VectorDouble vector, int shift)
        {
            VectorDouble result = new VectorDouble(vector.Size);

            for (int i = 0; i < result.Size; i++)
            {
                result[i] = (double)((int)vector[i] << shift);
            }

            return result;
        }

        public static bool operator ==(VectorDouble vector1, VectorDouble vector2)
        {
            if (object.ReferenceEquals(vector1, null) || object.ReferenceEquals(vector2, null))
            {
                return false;
            }

            if (vector1.Size != vector2.Size)
            {
                return false;
            }

            for (int i = 0; i < vector1.Size; i++)
            {
                if (vector1[i] != vector2[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator !=(VectorDouble vector1, VectorDouble vector2)
        {
            return !(vector1 == vector2);
        }

        public static bool operator >(VectorDouble vector1, VectorDouble vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                Console.WriteLine("Vectors must have the same size to perform comparison.");
                return false;
            }

            for (int i = 0; i < vector1.Size; i++)
            {
                if (vector1[i] <= vector2[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator >=(VectorDouble vector1, VectorDouble vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                Console.WriteLine("Vectors must have the same size to perform comparison.");
                return false;
            }

            for (int i = 0; i < vector1.Size; i++)
            {
                if (vector1[i] < vector2[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator <(VectorDouble vector1, VectorDouble vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                Console.WriteLine("Vectors must have the same size to perform comparison.");
                return false;
            }

            for (int i = 0; i < vector1.Size; i++)
            {
                if (vector1[i] >= vector2[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator <=(VectorDouble vector1, VectorDouble vector2)
        {
            if (vector1.Size != vector2.Size)
            {
                Console.WriteLine("Vectors must have the same size to perform comparison.");
                return false;
            }

            for (int i = 0; i < vector1.Size; i++)
            {
                if (vector1[i] > vector2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }

    static void task2()
    {
        VectorDouble vector = new VectorDouble(3);

        Console.WriteLine("Enter elements of the vector:");
        vector.InputElements();

        vector.PrintElements();

        vector.AssignValue(5.0);

        vector.PrintElements();

        Console.WriteLine($"Number of vectors: {VectorDouble.CountVectors()}");
        Console.WriteLine("Accessing vector elements using indexer:");
        for (int i = 0; i < vector.Size + 1; i++)
        {
            Console.WriteLine($"Element at index {i}: {vector[i]}, Error code: {vector.CodeError}");
        }

        VectorDouble vector1 = new VectorDouble(3);
        VectorDouble vector2 = new VectorDouble(3);
        double scalar = 10.0;

        Console.WriteLine("Enter elements of the first vector:");
        vector1.InputElements();

        Console.WriteLine("Enter elements of the second vector:");
        vector2.InputElements();

        VectorDouble vectorSum = vector1 + vector2;
        if (vectorSum != null)
        {
            Console.WriteLine("Sum of vectors:");
            vectorSum.PrintElements();
        }
        VectorDouble vectorScalarSum = vector1 + scalar;
        Console.WriteLine("Sum of first vector and scalar:");
        vectorScalarSum.PrintElements();

        VectorDouble vectorMinus = vector1 - vector2;
        if (vectorMinus != null)
        {
            Console.WriteLine("Difference of vectors:");
            vectorMinus.PrintElements();
        }
        VectorDouble vectorScalarMinus = vector1 - scalar;
        Console.WriteLine("Difference of first vector and scalar:");
        vectorScalarMinus.PrintElements();

        VectorDouble vectorMult = vector1 * vector2;
        if (vectorMult != null)
        {
            Console.WriteLine("Multiplication of vectors:");
            vectorMult.PrintElements();
        }
        VectorDouble vectorScalarMult = vector1 * scalar;
        Console.WriteLine("Multiplication of first vector by scalar:");
        vectorScalarMult.PrintElements();

        VectorDouble vectorDivision = vector1 / vector2;
        if (vectorDivision != null)
        {
            Console.WriteLine("Element-wise division of vectors:");
            vectorDivision.PrintElements();
        }
        VectorDouble vectorScalarDivision = vector1 / scalar;
        Console.WriteLine("Division of first vector by scalar:");
        vectorScalarDivision.PrintElements();

        VectorDouble vectorRem = vector1 % vector2;
        if (vectorRem != null)
        {
            Console.WriteLine("Element-wise remainder of vectors:");
            vectorRem.PrintElements();
        }
        VectorDouble vectorScalarRem = vector1 % scalar;
        Console.WriteLine("Remainder of first vector by scalar:");
        vectorScalarRem.PrintElements();

        Console.WriteLine("\nBitwise OR operation between vector1 and vector2:");
        VectorDouble bitwiseOR = vector1 | vector2;
        bitwiseOR.PrintElements();

        Console.WriteLine("\nBitwise OR operation between vector1 and scalar:");
        VectorDouble bitwiseORScalar = vector1 | 5.0;
        bitwiseORScalar.PrintElements();

        Console.WriteLine("\nBitwise XOR operation between vector1 and vector2:");
        VectorDouble bitwiseXOR = vector1 ^ vector2;
        bitwiseXOR.PrintElements();

        Console.WriteLine("\nBitwise XOR operation between vector1 and scalar:");
        VectorDouble bitwiseXORScalar = vector1 ^ 5.0;
        bitwiseXORScalar.PrintElements();

        Console.WriteLine("\nBitwise AND operation between vector1 and vector2:");
        VectorDouble bitwiseAND = vector1 & vector2;
        bitwiseAND.PrintElements();

        Console.WriteLine("\nBitwise AND operation between vector1 and scalar:");
        VectorDouble bitwiseANDScalar = vector1 & 5.0;
        bitwiseANDScalar.PrintElements();

        Console.WriteLine("\nBitwise right shift operation of vector1 by 2 bits:");
        VectorDouble rightShift = vector1 >> 2;
        rightShift.PrintElements();

        Console.WriteLine("\nBitwise left shift operation of vector1 by 2 bits:");
        VectorDouble leftShift = vector1 << 2;
        leftShift.PrintElements();

        Console.WriteLine("\nEquality check between vector1 and vector2:");
        Console.WriteLine(vector1 == vector2);

        Console.WriteLine("\nInequality check between vector1 and vector2:");
        Console.WriteLine(vector1 != vector2);

        Console.WriteLine("\nComparison: vector1 greater than vector2:");
        Console.WriteLine(vector1 > vector2);

        Console.WriteLine("\nComparison: vector1 greater than or equal to vector2:");
        Console.WriteLine(vector1 >= vector2);

        Console.WriteLine("\nComparison: vector1 less than vector2:");
        Console.WriteLine(vector1 < vector2);

        Console.WriteLine("\nComparison: vector1 less than or equal to vector2:");
        Console.WriteLine(vector1 <= vector2);
    }

    class MatrixDouble
    {
        protected double[,] DArray;
        protected uint n, m;
        protected int codeError;
        protected static int num_mf;

        public MatrixDouble()
        {
            n = 1;
            m = 1;
            DArray = new double[n, m];
            codeError = 0;
            num_mf++;
        }

        public MatrixDouble(uint n, uint m)
        {
            this.n = n;
            this.m = m;
            DArray = new double[n, m];
            codeError = 0;
            num_mf++;
        }

        public MatrixDouble(uint n, uint m, double initialValue)
        {
            this.n = n;
            this.m = m;
            DArray = new double[n, m];
            for (uint i = 0; i < n; i++)
            {
                for (uint j = 0; j < m; j++)
                {
                    DArray[i, j] = initialValue;
                }
            }
            codeError = 0;
            num_mf++;
        }

        ~MatrixDouble()
        {
            Console.WriteLine("Matrix has been destructed.");
        }

        public void InputElements()
        {
            for (uint i = 0; i < n; i++)
            {
                for (uint j = 0; j < m; j++)
                {
                    Console.Write("Element [{0}, {1}]: ", i, j);
                    DArray[i, j] = Convert.ToDouble(Console.ReadLine());
                }
            }
        }

        public void DisplayElements()
        {
            for (uint i = 0; i < n; i++)
            {
                for (uint j = 0; j < m; j++)
                {
                    Console.Write(DArray[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        public void Assign(double value)
        {
            for (uint i = 0; i < n; i++)
            {
                for (uint j = 0; j < m; j++)
                {
                    DArray[i, j] = value;
                }
            }
        }

        public static int CountMatrices()
        {
            return num_mf;
        }

        public uint[] Dimensions
        {
            get { return new uint[] { n, m }; }
        }

        public int ErrorCode
        {
            get { return codeError; }
            set { codeError = value; }
        }

        public double this[uint i, uint j]
        {
            get
            {
                if (i < n && j < m)
                    return DArray[i, j];
                else
                {
                    codeError = -1;
                    return 0;
                }
            }
            set
            {
                if (i < n && j < m)
                    DArray[i, j] = value;
                else
                    codeError = -1;
            }
        }

        public double this[uint k]
        {
            get
            {
                uint i = k / m;
                uint j = k % m;
                if (i < n && j < m)
                    return DArray[i, j];
                else
                {
                    codeError = -1;
                    return 0;
                }
            }
            set
            {
                uint i = k / m;
                uint j = k % m;
                if (i < n && j < m)
                    DArray[i, j] = value;
                else
                    codeError = -1;
            }
        }

        public static MatrixDouble operator ++(MatrixDouble md)
        {
            for (uint i = 0; i < md.n; i++)
            {
                for (uint j = 0; j < md.m; j++)
                {
                    md.DArray[i, j]++;
                }
            }
            return md;
        }

        public static MatrixDouble operator --(MatrixDouble md)
        {
            for (uint i = 0; i < md.n; i++)
            {
                for (uint j = 0; j < md.m; j++)
                {
                    md.DArray[i, j]--;
                }
            }
            return md;
        }

        public static bool operator true(MatrixDouble md)
        {
            for (uint i = 0; i < md.n; i++)
            {
                for (uint j = 0; j < md.m; j++)
                {
                    if (md.DArray[i, j] == 0)
                        return false;
                }
            }
            return true;
        }

        public static bool operator false(MatrixDouble md)
        {
            for (uint i = 0; i < md.n; i++)
            {
                for (uint j = 0; j < md.m; j++)
                {
                    if (md.DArray[i, j] == 0)
                        return true;
                }
            }
            return false;
        }

        public static bool operator !(MatrixDouble md)
        {
            return md.n != 0 && md.m != 0;
        }

        public static MatrixDouble operator ~(MatrixDouble md)
        {
            MatrixDouble result = new MatrixDouble(md.n, md.m);
            for (uint i = 0; i < md.n; i++)
            {
                for (uint j = 0; j < md.m; j++)
                {
                    long intValue = BitConverter.DoubleToInt64Bits(md.DArray[i, j]);
                    intValue = ~intValue;
                    result.DArray[i, j] = BitConverter.Int64BitsToDouble(intValue);
                }
            }
            return result;
        }

        public static MatrixDouble operator +(MatrixDouble matrix1, MatrixDouble matrix2)
        {
            if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            {
                Console.WriteLine("Matrices must have the same dimensions to perform addition.");
                return null;
            }

            MatrixDouble result = new MatrixDouble(matrix1.n, matrix1.m);

            for (uint i = 0; i < matrix1.n; i++)
            {
                for (uint j = 0; j < matrix1.m; j++)
                {
                    result.DArray[i, j] = matrix1.DArray[i, j] + matrix2.DArray[i, j];
                }
            }

            return result;
        }

        public static MatrixDouble operator +(MatrixDouble matrix, double scalar)
        {
            MatrixDouble result = new MatrixDouble(matrix.n, matrix.m);

            for (uint i = 0; i < matrix.n; i++)
            {
                for (uint j = 0; j < matrix.m; j++)
                {
                    result.DArray[i, j] = matrix.DArray[i, j] + scalar;
                }
            }

            return result;
        }

        public static MatrixDouble operator -(MatrixDouble matrix1, MatrixDouble matrix2)
        {
            if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            {
                Console.WriteLine("Matrices must have the same dimensions to perform subtraction.");
                return null;
            }

            MatrixDouble result = new MatrixDouble(matrix1.n, matrix1.m);

            for (uint i = 0; i < matrix1.n; i++)
            {
                for (uint j = 0; j < matrix1.m; j++)
                {
                    result.DArray[i, j] = matrix1.DArray[i, j] - matrix2.DArray[i, j];
                }
            }

            return result;
        }

        public static MatrixDouble operator -(MatrixDouble matrix, double scalar)
        {
            MatrixDouble result = new MatrixDouble(matrix.n, matrix.m);

            for (uint i = 0; i < matrix.n; i++)
            {
                for (uint j = 0; j < matrix.m; j++)
                {
                    result.DArray[i, j] = matrix.DArray[i, j] - scalar;
                }
            }

            return result;
        }

        public static MatrixDouble operator *(MatrixDouble matrix1, MatrixDouble matrix2)
        {
            if (matrix1.m != matrix2.n)
            {
                Console.WriteLine("The number of columns in the first matrix must be equal to the number of rows in the second matrix for multiplication.");
                return null;
            }

            MatrixDouble result = new MatrixDouble(matrix1.n, matrix2.m);

            for (uint i = 0; i < matrix1.n; i++)
            {
                for (uint j = 0; j < matrix2.m; j++)
                {
                    double sum = 0;
                    for (uint k = 0; k < matrix1.m; k++)
                    {
                        sum += matrix1.DArray[i, k] * matrix2.DArray[k, j];
                    }
                    result.DArray[i, j] = sum;
                }
            }

            return result;
        }

        public static MatrixDouble operator *(MatrixDouble matrix, double scalar)
        {
            MatrixDouble result = new MatrixDouble(matrix.n, matrix.m);

            for (uint i = 0; i < matrix.n; i++)
            {
                for (uint j = 0; j < matrix.m; j++)
                {
                    result.DArray[i, j] = matrix.DArray[i, j] * scalar;
                }
            }

            return result;
        }

        public static MatrixDouble operator /(MatrixDouble matrix1, MatrixDouble matrix2)
        {
            if (matrix1.m != matrix2.n)
            {
                Console.WriteLine("The number of columns in the first matrix must be equal to the number of rows in the second matrix for division.");
                return null;
            }

            // Assuming matrix2 is invertible
            // For simplicity, let's just return the product of matrix1 and the inverse of matrix2
            return matrix1 * Inverse(matrix2);
        }

        public static MatrixDouble operator /(MatrixDouble matrix, double scalar)
        {
            MatrixDouble result = new MatrixDouble(matrix.n, matrix.m);

            for (uint i = 0; i < matrix.n; i++)
            {
                for (uint j = 0; j < matrix.m; j++)
                {
                    result.DArray[i, j] = matrix.DArray[i, j] / scalar;
                }
            }

            return result;
        }

        public static MatrixDouble operator %(MatrixDouble matrix1, MatrixDouble matrix2)
        {
            if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            {
                Console.WriteLine("Matrices must have the same dimensions to perform modulo operation.");
                return null;
            }

            MatrixDouble result = new MatrixDouble(matrix1.n, matrix1.m);

            for (uint i = 0; i < matrix1.n; i++)
            {
                for (uint j = 0; j < matrix1.m; j++)
                {
                    result.DArray[i, j] = matrix1.DArray[i, j] % matrix2.DArray[i, j];
                }
            }

            return result;
        }

        public static MatrixDouble operator %(MatrixDouble matrix, uint scalar)
        {
            MatrixDouble result = new MatrixDouble(matrix.n, matrix.m);

            for (uint i = 0; i < matrix.n; i++)
            {
                for (uint j = 0; j < matrix.m; j++)
                {
                    result.DArray[i, j] = matrix.DArray[i, j] % scalar;
                }
            }

            return result;
        }

        public static MatrixDouble operator |(MatrixDouble matrix1, MatrixDouble matrix2)
        {
            if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            {
                Console.WriteLine("Matrices must have the same dimensions to perform bitwise OR operation.");
                return null;
            }

            MatrixDouble result = new MatrixDouble(matrix1.n, matrix1.m);

            for (uint i = 0; i < matrix1.n; i++)
            {
                for (uint j = 0; j < matrix1.m; j++)
                {
                    long intValue1 = BitConverter.DoubleToInt64Bits(matrix1.DArray[i, j]);
                    long intValue2 = BitConverter.DoubleToInt64Bits(matrix2.DArray[i, j]);
                    long resultValue = intValue1 | intValue2;
                    result.DArray[i, j] = BitConverter.Int64BitsToDouble(resultValue);
                }
            }

            return result;
        }

        public static MatrixDouble operator |(MatrixDouble matrix, double scalar)
        {
            MatrixDouble result = new MatrixDouble(matrix.n, matrix.m);

            long scalarValue = BitConverter.DoubleToInt64Bits(scalar);

            for (uint i = 0; i < matrix.n; i++)
            {
                for (uint j = 0; j < matrix.m; j++)
                {
                    long intValue = BitConverter.DoubleToInt64Bits(matrix.DArray[i, j]);
                    long resultValue = intValue | scalarValue;
                    result.DArray[i, j] = BitConverter.Int64BitsToDouble(resultValue);
                }
            }

            return result;
        }

        public static MatrixDouble operator ^(MatrixDouble matrix1, MatrixDouble matrix2)
        {
            if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            {
                Console.WriteLine("Matrices must have the same dimensions to perform bitwise XOR operation.");
                return null;
            }

            MatrixDouble result = new MatrixDouble(matrix1.n, matrix1.m);

            for (uint i = 0; i < matrix1.n; i++)
            {
                for (uint j = 0; j < matrix1.m; j++)
                {
                    long intValue1 = BitConverter.DoubleToInt64Bits(matrix1.DArray[i, j]);
                    long intValue2 = BitConverter.DoubleToInt64Bits(matrix2.DArray[i, j]);
                    long resultValue = intValue1 ^ intValue2;
                    result.DArray[i, j] = BitConverter.Int64BitsToDouble(resultValue);
                }
            }

            return result;
        }

        public static MatrixDouble operator ^(MatrixDouble matrix, double scalar)
        {
            MatrixDouble result = new MatrixDouble(matrix.n, matrix.m);

            long scalarValue = BitConverter.DoubleToInt64Bits(scalar);

            for (uint i = 0; i < matrix.n; i++)
            {
                for (uint j = 0; j < matrix.m; j++)
                {
                    long intValue = BitConverter.DoubleToInt64Bits(matrix.DArray[i, j]);
                    long resultValue = intValue ^ scalarValue;
                    result.DArray[i, j] = BitConverter.Int64BitsToDouble(resultValue);
                }
            }

            return result;
        }

        public static MatrixDouble operator &(MatrixDouble matrix, ushort scalar)
        {
            MatrixDouble result = new MatrixDouble(matrix.n, matrix.m);

            for (uint i = 0; i < matrix.n; i++)
            {
                for (uint j = 0; j < matrix.m; j++)
                {
                    long intValue = BitConverter.DoubleToInt64Bits(matrix.DArray[i, j]);
                    long resultValue = intValue & scalar;
                    result.DArray[i, j] = BitConverter.Int64BitsToDouble(resultValue);
                }
            }

            return result;
        }

        public static MatrixDouble operator >>(MatrixDouble matrix, int shift)
        {
            MatrixDouble result = new MatrixDouble(matrix.n, matrix.m);

            for (uint i = 0; i < matrix.n; i++)
            {
                for (uint j = 0; j < matrix.m; j++)
                {
                    long intValue = BitConverter.DoubleToInt64Bits(matrix.DArray[i, j]);
                    long resultValue = intValue >> shift;
                    result.DArray[i, j] = BitConverter.Int64BitsToDouble(resultValue);
                }
            }

            return result;
        }

        public static MatrixDouble operator <<(MatrixDouble matrix, int shift)
        {
            MatrixDouble result = new MatrixDouble(matrix.n, matrix.m);

            for (uint i = 0; i < matrix.n; i++)
            {
                for (uint j = 0; j < matrix.m; j++)
                {
                    long intValue = BitConverter.DoubleToInt64Bits(matrix.DArray[i, j]);
                    long resultValue = intValue << shift;
                    result.DArray[i, j] = BitConverter.Int64BitsToDouble(resultValue);
                }
            }

            return result;
        }

        public static bool operator ==(MatrixDouble matrix1, MatrixDouble matrix2)
        {
            if (ReferenceEquals(matrix1, matrix2))
            {
                return true;
            }

            if (matrix1 is null || matrix2 is null)
            {
                return false;
            }

            if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            {
                return false;
            }

            for (uint i = 0; i < matrix1.n; i++)
            {
                for (uint j = 0; j < matrix1.m; j++)
                {
                    if (matrix1.DArray[i, j] != matrix2.DArray[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator !=(MatrixDouble matrix1, MatrixDouble matrix2)
        {
            return !(matrix1 == matrix2);
        }

        public static bool operator >(MatrixDouble matrix1, MatrixDouble matrix2)
        {
            if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            {
                return false;
            }

            for (uint i = 0; i < matrix1.n; i++)
            {
                for (uint j = 0; j < matrix1.m; j++)
                {
                    if (matrix1.DArray[i, j] <= matrix2.DArray[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator >=(MatrixDouble matrix1, MatrixDouble matrix2)
        {
            if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            {
                return false;
            }

            for (uint i = 0; i < matrix1.n; i++)
            {
                for (uint j = 0; j < matrix1.m; j++)
                {
                    if (matrix1.DArray[i, j] < matrix2.DArray[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator <(MatrixDouble matrix1, MatrixDouble matrix2)
        {
            if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            {
                return false;
            }

            for (uint i = 0; i < matrix1.n; i++)
            {
                for (uint j = 0; j < matrix1.m; j++)
                {
                    if (matrix1.DArray[i, j] >= matrix2.DArray[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator <=(MatrixDouble matrix1, MatrixDouble matrix2)
        {
            if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            {
                return false;
            }

            for (uint i = 0; i < matrix1.n; i++)
            {
                for (uint j = 0; j < matrix1.m; j++)
                {
                    if (matrix1.DArray[i, j] > matrix2.DArray[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static MatrixDouble Inverse(MatrixDouble matrix)
        {
            Console.WriteLine("Inverse not implemented. Returning the original matrix.");
            return matrix;
        }
    }

    public static void task3()
    {
        MatrixDouble matrix = new MatrixDouble(2, 3, 3.0);
        Console.WriteLine("Size of matrix: {0}x{1}", matrix.Dimensions[0], matrix.Dimensions[1]);
        Console.WriteLine("Element of matrix:");
        matrix.DisplayElements();

        matrix[0, 1] = 10.0;
        Console.WriteLine("\nMatrix after change [0, 1]:");
        matrix.DisplayElements();

        Console.WriteLine("\nChange all elements to 8.0:");
        matrix.Assign(8.0);
        matrix.DisplayElements();

        Console.WriteLine("\nEnter new element:");
        matrix.InputElements();
        Console.WriteLine("\nElement after change:");
        matrix.DisplayElements();

        Console.WriteLine("\nThe number of matrix: {0}", MatrixDouble.CountMatrices());

        MatrixDouble matrix1 = new MatrixDouble(2, 3, 7.0);
        Console.WriteLine("Matrix1 elements:");
        matrix1.DisplayElements();

        MatrixDouble matrix2 = new MatrixDouble(2, 3, 1.0);
        Console.WriteLine("\nMatrix2 elements:");
        matrix2.DisplayElements();


        Console.WriteLine("Size of matrix1: {0}x{1}", matrix1.Dimensions[0], matrix1.Dimensions[1]);
        Console.WriteLine("Size of matrix2: {0}x{1}", matrix2.Dimensions[0], matrix2.Dimensions[1]);


        MatrixDouble matrixSum = matrix1 + matrix2;
        if (matrixSum != null)
        {
            Console.WriteLine("\nSum of matrices:");
            matrixSum.DisplayElements();
        }

        MatrixDouble matrixScalarSum = matrix1 + 10.0;
        Console.WriteLine("\nSum of matrix1 and scalar:");
        matrixScalarSum.DisplayElements();

        MatrixDouble matrixDiff = matrix1 - matrix2;
        if (matrixDiff != null)
        {
            Console.WriteLine("\nDifference of matrices:");
            matrixDiff.DisplayElements();
        }

        MatrixDouble matrixScalarDiff = matrix1 - 5.0;
        Console.WriteLine("\nDifference of matrix1 and scalar:");
        matrixScalarDiff.DisplayElements();

        MatrixDouble matrixProduct = matrix1 * matrix2;
        if (matrixProduct != null)
        {
            Console.WriteLine("\nElement-wise multiplication of matrices:");
            matrixProduct.DisplayElements();
        }

        MatrixDouble matrixScalarProduct = matrix1 * 2.0;
        Console.WriteLine("\nMultiplication of matrix1 by scalar:");
        matrixScalarProduct.DisplayElements();

        MatrixDouble matrixDivision = matrix1 / matrix2;
        if (matrixDivision != null)
        {
            Console.WriteLine("\nElement-wise division of matrices:");
            matrixDivision.DisplayElements();
        }

        MatrixDouble matrixScalarDivision = matrix1 / 2.0;
        Console.WriteLine("\nDivision of matrix1 by scalar:");
        matrixScalarDivision.DisplayElements();

        MatrixDouble matrixRemainder = matrix1 % matrix2;
        if (matrixRemainder != null)
        {
            Console.WriteLine("\nElement-wise remainder of matrices:");
            matrixRemainder.DisplayElements();
        }

        MatrixDouble matrixScalarRemainder = matrix1 % 3U;
        Console.WriteLine("\nRemainder of matrix1 by scalar:");
        matrixScalarRemainder.DisplayElements();

        Console.WriteLine("\nBitwise OR operation between matrix1 and matrix2:");
        MatrixDouble bitwiseOR = matrix1 | matrix2;
        bitwiseOR.DisplayElements();

        Console.WriteLine("\nBitwise OR operation between matrix1 and scalar:");
        MatrixDouble bitwiseORScalar = matrix1 | 5.0;
        bitwiseORScalar.DisplayElements();

        Console.WriteLine("\nBitwise XOR operation between matrix1 and matrix2:");
        MatrixDouble bitwiseXOR = matrix1 ^ matrix2;
        bitwiseXOR.DisplayElements();

        Console.WriteLine("\nBitwise XOR operation between matrix1 and scalar:");
        MatrixDouble bitwiseXORScalar = matrix1 ^ 5.0;
        bitwiseXORScalar.DisplayElements();

        Console.WriteLine("\nBitwise AND operation between matrix1 and scalar:");
        MatrixDouble bitwiseANDScalar = matrix1 & (ushort)5;
        bitwiseANDScalar.DisplayElements();

        Console.WriteLine("\nBitwise right shift operation of matrix1 by 2 bits:");
        MatrixDouble rightShift = matrix1 >> 2;
        rightShift.DisplayElements();

        Console.WriteLine("\nBitwise left shift operation of matrix1 by 2 bits:");
        MatrixDouble leftShift = matrix1 << 2;
        leftShift.DisplayElements();

        Console.WriteLine("\nEquality check between matrix1 and matrix2:");
        Console.WriteLine(matrix1 == matrix2);

        Console.WriteLine("\nInequality check between matrix1 and matrix2:");
        Console.WriteLine(matrix1 != matrix2);

        Console.WriteLine("\nComparison: matrix1 greater than matrix2:");
        Console.WriteLine(matrix1 > matrix2);

        Console.WriteLine("\nComparison: matrix1 greater than or equal to matrix2:");
        Console.WriteLine(matrix1 >= matrix2);

        Console.WriteLine("\nComparison: matrix1 less than matrix2:");
        Console.WriteLine(matrix1 < matrix2);

        Console.WriteLine("\nComparison: matrix1 less than or equal to matrix2:");
        Console.WriteLine(matrix1 <= matrix2);

        Console.WriteLine("\nThe number of matrices: {0}", MatrixDouble.CountMatrices());
    }
}
