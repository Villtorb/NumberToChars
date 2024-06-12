using NumberToChars;

int i = 1729;
var int_result = ToChars.Int(i, stackalloc char[32]);
// var int_result = i.IntToChars(stackalloc char[32]);
Console.WriteLine($"int:\t{int_result}");

float f = 3.141592f;
var float_result = ToChars.Float(f, stackalloc char[32]);
// var float_result = f.FloatToChars(stackalloc char[32]);
Console.WriteLine($"float:\t{float_result}");

double d = 1.61803398874989d;
var double_result = ToChars.Double(d, stackalloc char[32]);
// var double_result = d.DoubleToChars(stackalloc char[32]);
Console.WriteLine($"double:\t{double_result}");
