using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumberToChars;

namespace NumberToChars.Tests;

[TestClass]
public class ToCharsTests
{
    [TestMethod]
    public void Int_MoreDigitsThanSpanLength_Throws()
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            int value = 1234;
            Span<char> buffer = stackalloc char[3];
            ToChars.Int(value, buffer);
        });
    }

    [TestMethod]
    public void Int_Positive()
    {
        int value = 123;
        Span<char> buffer = stackalloc char[8];
        Span<char> result = ToChars.Int(value, buffer);

        var expected_buffer = new char[] { '\0', '\0', '\0', '\0', '\0', '1', '2', '3' };
        var expected_result = new char[] { '1', '2', '3' };
        var got_buffer = buffer.ToArray();
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(expected_buffer, got_buffer, expected_buffer_Got_Msg(ColStr(expected_buffer), ColStr(got_buffer)));
        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
    }

    [TestMethod]
    public void Int_Negative()
    {
        int value = -123;
        Span<char> buffer = stackalloc char[8];
        Span<char> result = ToChars.Int(value, buffer);

        var expected_buffer = new char[] { '\0', '\0', '\0', '\0', '-', '1', '2', '3' };
        var expected_result = new char[] { '-', '1', '2', '3' };
        var got_buffer = buffer.ToArray();
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(expected_buffer, got_buffer, expected_buffer_Got_Msg(ColStr(expected_buffer), ColStr(got_buffer)));
        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
    }

    [TestMethod]
    public void Int_Zero()
    {
        int value = -0;
        Span<char> buffer = stackalloc char[8];
        Span<char> result = ToChars.Int(value, buffer);

        var expected_buffer = new char[] { '\0', '\0', '\0', '\0', '\0', '\0', '\0', '0' };
        var expected_result = new char[] { '0' };
        var got_buffer = buffer.ToArray();
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(expected_buffer, got_buffer, expected_buffer_Got_Msg(ColStr(expected_buffer), ColStr(got_buffer)));
        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
    }

    [TestMethod]
    public void Int_Negative_FillSpan()
    {
        int value = -1234567;
        Span<char> buffer = stackalloc char[8];
        Span<char> result = ToChars.Int(value, buffer);

        var expected_buffer = new char[] { '-', '1', '2', '3', '4', '5', '6', '7' };
        var expected_result = new char[] { '-', '1', '2', '3', '4', '5', '6', '7' };
        var got_buffer = buffer.ToArray();
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(got_buffer, got_result, expected_buffer_Got_Msg(ColStr(got_buffer), ColStr(got_result)));
        CollectionAssert.AreEquivalent(expected_buffer, got_buffer, expected_buffer_Got_Msg(ColStr(expected_buffer), ColStr(got_buffer)));
        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
    }

    [TestMethod]
    public void Float_Zero()
    {
        float value = 0f;
        Span<char> buffer = stackalloc char[8];
        Span<char> result = ToChars.Float(value, buffer);

        var expected_buffer = new char[] { '0', '\0', '\0', '\0', '\0', '\0', '\0', '\0' };
        var expected_result = new char[] { '0' };
        var got_buffer = buffer.ToArray();
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
        CollectionAssert.AreEquivalent(expected_buffer, got_buffer, expected_buffer_Got_Msg(ColStr(expected_buffer), ColStr(got_buffer)));
    }

    [TestMethod]
    public void Float_Negative_PreciseTo_7Digits()
    {
        float value = -3.141592653589793f * 100;
        Span<char> buffer = stackalloc char[16];
        Span<char> result = ToChars.Float(value, buffer);

        var expected_buffer = new char[] { '-', '3', '1', '4', '.', '1', '5', '9', '2', '\0', '\0', '\0', '\0', '\0', '\0', '\0' };
        var expected_result = new char[] { '-', '3', '1', '4', '.', '1', '5', '9', '2' };
        var got_buffer = buffer.ToArray();
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
        CollectionAssert.AreEquivalent(expected_buffer, got_buffer, expected_buffer_Got_Msg(ColStr(expected_buffer), ColStr(got_buffer)));
    }

    [TestMethod]
    public void Float_PreciseTo_7Digits()
    {
        float value = 2568.52148437500f;
        Span<char> buffer = stackalloc char[16];
        Span<char> result = ToChars.Float(value, buffer);

        var expected_buffer = new char[] { '2', '5', '6', '8', '.', '5', '2', '1', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0' };
        var expected_result = new char[] { '2', '5', '6', '8', '.', '5', '2', '1' };
        var got_buffer = buffer.ToArray();
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
        CollectionAssert.AreEquivalent(expected_buffer, got_buffer, expected_buffer_Got_Msg(ColStr(expected_buffer), ColStr(got_buffer)));
    }

    public void Double_Zero()
    {
        double value = 0d;
        Span<char> buffer = stackalloc char[8];
        Span<char> result = ToChars.Double(value, buffer);

        var expected_buffer = new char[] { '0', '\0', '\0', '\0', '\0', '\0', '\0', '\0' };
        var expected_result = new char[] { '0' };
        var got_buffer = buffer.ToArray();
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
        CollectionAssert.AreEquivalent(expected_buffer, got_buffer, expected_buffer_Got_Msg(ColStr(expected_buffer), ColStr(got_buffer)));
    }

    [TestMethod]
    public void Double_PreciseTo_15Digits()
    {
        double value = 6283.185307179582d;
        Span<char> buffer = stackalloc char[20];
        Span<char> result = ToChars.Double(value, buffer);

        var expected_buffer = new char[] { '6', '2', '8', '3', '.', '1', '8', '5', '3', '0', '7', '1', '7', '9', '5', '8', '\0', '\0', '\0', '\0' };
        var expected_result = new char[] { '6', '2', '8', '3', '.', '1', '8', '5', '3', '0', '7', '1', '7', '9', '5', '8' };
        var got_buffer = buffer.ToArray();
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
        CollectionAssert.AreEquivalent(expected_buffer, got_buffer, expected_buffer_Got_Msg(ColStr(expected_buffer), ColStr(got_buffer)));
    }

    [TestMethod]
    public void Double_Negative_PreciseTo_15Digits()
    {
        double value = -314.1592653589793d;
        Span<char> buffer = stackalloc char[20];
        Span<char> result = ToChars.Double(value, buffer);

        var expected_buffer = new char[] { '-', '3', '1', '4', '.', '1', '5', '9', '2', '6', '5', '3', '5', '8', '9', '7', '9', '\0', '\0', '\0' };
        var expected_result = new char[] { '-', '3', '1', '4', '.', '1', '5', '9', '2', '6', '5', '3', '5', '8', '9', '7', '9' };
        var got_buffer = buffer.ToArray();
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
        CollectionAssert.AreEquivalent(expected_buffer, got_buffer, expected_buffer_Got_Msg(ColStr(expected_buffer), ColStr(got_buffer)));
    }

    [TestMethod]
    public void Double_Frac_PreciseTo_15Digits()
    {
        double value = 0.03141592653589793d;
        Span<char> buffer = stackalloc char[20];
        Span<char> result = ToChars.Double(value, buffer);

        var expected_buffer = new char[] { '0', '.', '0', '3', '1', '4', '1', '5', '9', '2', '6', '5', '3', '5', '8', '9', '7', '9', '\0', '\0' };
        var expected_result = new char[] { '0', '.', '0', '3', '1', '4', '1', '5', '9', '2', '6', '5', '3', '5', '8', '9', '7', '9' };
        var got_buffer = buffer.ToArray();
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
        CollectionAssert.AreEquivalent(expected_buffer, got_buffer, expected_buffer_Got_Msg(ColStr(expected_buffer), ColStr(got_buffer)));
    }

    [TestMethod]
    public void Float_MoreDigitsThanPrecision_OmitFractionalPart()
    {
        float value = 3141592.653589793f;
        Span<char> buffer = stackalloc char[16];
        Span<char> result = ToChars.Float(value, buffer);

        var expected_buffer = new char[] { '3', '1', '4', '1', '5', '9', '2', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0' };
        var expected_result = new char[] { '3', '1', '4', '1', '5', '9', '2' };
        var got_buffer = buffer.ToArray();
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
        CollectionAssert.AreEquivalent(expected_buffer, got_buffer, expected_buffer_Got_Msg(ColStr(expected_buffer), ColStr(got_buffer)));
    }

    [TestMethod]
    public void Float_MoreIntDigitsThanSpanLength_Throws()
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            float value = 1234;
            Span<char> buffer = stackalloc char[3];
            ToChars.Float(value, buffer);
        });
    }

    [TestMethod]
    public void Fraction_MoreDigitsThanSpanLength_DoesNotThrow()
    {
        float value = 0.00001234567f;
        Span<char> buffer = stackalloc char[6];
        Span<char> result = ToChars.Float(value, buffer);

        var expected_buffer = new char[] { '0', '.', '0', '0', '0', '0' };
        var expected_result = new char[] { '0', '.', '0', '0', '0', '0' };
        var got_buffer = buffer.ToArray();
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
        CollectionAssert.AreEquivalent(expected_buffer, got_buffer, expected_buffer_Got_Msg(ColStr(expected_buffer), ColStr(got_buffer)));
    }

    [TestMethod]
    public void Fraction_LeadWith_Zero()
    {
        float value = 0.12345678f;
        Span<char> buffer = stackalloc char[9];
        Span<char> result = ToChars.Float(value, buffer);

        var expected_result = new char[] { '0', '.', '1', '2', '3', '4', '5', '6', '7' };
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
    }

    [TestMethod]
    public void Fraction_Negative_LeadWith_Zero()
    {
        float value = -0.1234567f;
        Span<char> buffer = stackalloc char[10];
        Span<char> result = ToChars.Float(value, buffer);

        var expected_result = new char[] { '-', '0', '.', '1', '2', '3', '4', '5', '6', '7' };
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
    }

    [TestMethod]
    public void Fraction_NegativeExponents_NoLossOfPrecision()
    {
        float value = 0.0000000000000001234567f;
        Span<char> buffer = stackalloc char[24];
        Span<char> result = ToChars.Float(value, buffer);

        var expected_buffer = new char[] { '0', '.', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '2', '3', '4', '5', '6', '7' };
        var expected_result = new char[] { '0', '.', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '2', '3', '4', '5', '6', '7' };
        var got_buffer = buffer.ToArray();
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
        CollectionAssert.AreEquivalent(expected_buffer, got_buffer, expected_buffer_Got_Msg(ColStr(expected_buffer), ColStr(got_buffer)));
    }

    [TestMethod]
    public void Float_NoFractionalPart_NoDecimalPoint()
    {
        float value = 1234f;
        Span<char> buffer = stackalloc char[8];
        Span<char> result = ToChars.Float(value, buffer);

        var expected_buffer = new char[] { '1', '2', '3', '4', '\0', '\0', '\0', '\0' };
        var expected_result = new char[] { '1', '2', '3', '4' };
        var got_buffer = buffer.ToArray();
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
        CollectionAssert.AreEquivalent(expected_buffer, got_buffer, expected_buffer_Got_Msg(ColStr(expected_buffer), ColStr(got_buffer)));
    }

    [TestMethod]
    public void Float_OneIndexLeftAfterInt_NoTrailingDecimalPoint()
    {
        float value = 1234.5678f;
        Span<char> buffer = stackalloc char[5];
        Span<char> result = ToChars.Float(value, buffer);

        var expected_buffer = new char[] { '1', '2', '3', '4', '\0' };
        var expected_result = new char[] { '1', '2', '3', '4' };
        var got_buffer = buffer.ToArray();
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
        CollectionAssert.AreEquivalent(expected_buffer, got_buffer, expected_buffer_Got_Msg(ColStr(expected_buffer), ColStr(got_buffer)));
    }

    [TestMethod]
    public void Float_IntExactlyFillsSpan_DoesNotThrow()
    {
        float value = 1234.5678f;
        Span<char> buffer = stackalloc char[4];
        Span<char> result = ToChars.Float(value, buffer);

        var expected_buffer = new char[] { '1', '2', '3', '4' };
        var expected_result = new char[] { '1', '2', '3', '4' };
        var got_buffer = buffer.ToArray();
        var got_result = result.ToArray();

        CollectionAssert.AreEquivalent(expected_result, got_result, expected_buffer_Got_Msg(ColStr(expected_result), ColStr(got_result)));
        CollectionAssert.AreEquivalent(expected_buffer, got_buffer, expected_buffer_Got_Msg(ColStr(expected_buffer), ColStr(got_buffer)));
    }

    //============================================================
    #region HELPERS
    //============================================================

    private static string expected_buffer_Got_Msg<Texpected_buffer, TGot>(Texpected_buffer expected_buffer, TGot got_buffer)
    {
        return $"\n\t-> Expected:\t{expected_buffer} \n\t-> Got:\t\t{got_buffer}";
    }

    private static string ColStr<T>(IEnumerable<T> col)
    {
        return $"{col.GetType()} : {String.Join(", ", col)}";
    }

    #endregion //-------------------------------------------------
}
