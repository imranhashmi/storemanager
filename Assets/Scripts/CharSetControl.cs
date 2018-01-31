using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSetControl 
{
	private enum charLinkAndDirection
	{
		NotLinkable = 0,
		RightLinked = 2,
		LeftLinked = 4,
		WidthLess = 8,
		noDirection = 16,
		numbers = 32
	}
    public struct strIndex
	{
		public string str;
		public bool leftToRight;
		public int orginalSize;
		public int strSize;
		public int strPos;
	}
	private class arCharSet
	{
		public string sChar;
        public string lChar;
        public string rChar;
        public string rlChar;
		public string nChar;
        public charLinkAndDirection Direction;
		public int charSize;
    }
	public struct csRes
	{
		public string convStr;
        public string orginalStr;
		public float[] charPlaces;
		public List<strIndex> strIndexs;
	}
    private List<arCharSet> mCharSet = new List<arCharSet>();
    private bool comparEnum(charLinkAndDirection val1, charLinkAndDirection val2) { return ((val1 & val2) == val2); }
    private static uint uniindex;
    private static CharSetControl _gCharSetControl = new CharSetControl();
    private static CharSetControl _control { get{return _gCharSetControl;} }
    private static Dictionary<string, csRes> arabicTable = new Dictionary<string, csRes>();
	private static char nlChar = '\n';
    private CharSetControl()
    {
        //// arabic 
        uniindex = 'ﷲ';
        ADD_CHAR(("الله"), charLinkAndDirection.NotLinkable);
        uniindex = 'ء';
        ADD_CHAR(("ء"), charLinkAndDirection.NotLinkable);
        uniindex = 65269;
        ADD_CHAR(("لآ"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("لأ"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("لإ"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("لا"), charLinkAndDirection.RightLinked);
        uniindex = 65153;
        ADD_CHAR(("آ"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("أ"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ؤ"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("إ"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ئ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ا"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ب"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ة"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ت"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ث"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ج"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ح"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("خ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("د"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ذ"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ر"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ز"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("س"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ش"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ص"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ض"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ط"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ظ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ع"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("غ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ف"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ق"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ك"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ل"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("م"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ن"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ه"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("و"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ى"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ي"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);

        //// urdo
        uniindex = 'ﭐ';
        ADD_CHAR(("ﭐ"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ﭒ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("پ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ﭚ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ﭞ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ﭢ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ٹ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ﭪ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ﭮ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ﭲ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ﭶ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("چ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ﭾ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ﮂ"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ﮄ"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ﮆ"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ڈ"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ژ"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ڑ"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ک"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("گ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ﮖ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ﮚ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ں"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ﮠ"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ﮠ"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ۀ"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ہ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        ADD_CHAR(("ھ"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
        uniindex = 'ﮮ';
        ADD_CHAR(("ے"), charLinkAndDirection.RightLinked);
        ADD_CHAR(("ۓ"), charLinkAndDirection.RightLinked);
        uniindex = 'ﯼ';
        ADD_CHAR(("ی"), charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);



        uniindex = '؟';
        ADD_CHAR(("؟"), charLinkAndDirection.NotLinkable);
        //////////// numbers 
        uniindex = '0';//'٠';
        ADD_CHAR(("0"), charLinkAndDirection.noDirection | charLinkAndDirection.numbers);
        ADD_CHAR(("1"), charLinkAndDirection.noDirection | charLinkAndDirection.numbers);
        ADD_CHAR(("2"), charLinkAndDirection.noDirection | charLinkAndDirection.numbers);
        ADD_CHAR(("3"), charLinkAndDirection.noDirection | charLinkAndDirection.numbers);
        ADD_CHAR(("4"), charLinkAndDirection.noDirection | charLinkAndDirection.numbers);
        ADD_CHAR(("5"), charLinkAndDirection.noDirection | charLinkAndDirection.numbers);
        ADD_CHAR(("6"), charLinkAndDirection.noDirection | charLinkAndDirection.numbers);
        ADD_CHAR(("7"), charLinkAndDirection.noDirection | charLinkAndDirection.numbers);
        ADD_CHAR(("8"), charLinkAndDirection.noDirection | charLinkAndDirection.numbers);
        ADD_CHAR(("9"), charLinkAndDirection.noDirection | charLinkAndDirection.numbers);
        uniindex = ')';
        ADD_CHAR(("("), charLinkAndDirection.noDirection);
        uniindex = '(';
        ADD_CHAR((")"), charLinkAndDirection.noDirection);
        uniindex = '؟';
        ADD_CHAR(("?"), charLinkAndDirection.noDirection);
        uniindex = '؛';
        ADD_CHAR((";"), charLinkAndDirection.noDirection);
        uniindex = 0;
        ADD_CHAR(("َ"), charLinkAndDirection.WidthLess);
        ADD_CHAR(("َ"), charLinkAndDirection.WidthLess);
        ADD_CHAR(("ً"), charLinkAndDirection.WidthLess);
        ADD_CHAR(("ُ"), charLinkAndDirection.WidthLess);
        ADD_CHAR(("ٌ"), charLinkAndDirection.WidthLess);
        ADD_CHAR(("ّ"), charLinkAndDirection.WidthLess);
        ADD_CHAR(("ِ"), charLinkAndDirection.WidthLess);
        ADD_CHAR(("ٍ"), charLinkAndDirection.WidthLess);
        ADD_CHAR(("ْ"), charLinkAndDirection.WidthLess);
        ADD_CHAR((" "), charLinkAndDirection.noDirection);
        ADD_CHAR((","), charLinkAndDirection.noDirection);
        ADD_CHAR(("."), charLinkAndDirection.noDirection);
        ADD_CHAR((":"), charLinkAndDirection.noDirection);
        ADD_CHAR(("،"), charLinkAndDirection.noDirection);
        ADD_CHAR(("_"), charLinkAndDirection.noDirection);
        ADD_CHAR(("&"), charLinkAndDirection.noDirection);
        ADD_CHAR(("^"), charLinkAndDirection.noDirection);
        ADD_CHAR(("%"), charLinkAndDirection.noDirection);
        ADD_CHAR(("$"), charLinkAndDirection.noDirection);
        ADD_CHAR(("#"), charLinkAndDirection.noDirection);
        ADD_CHAR(("@"), charLinkAndDirection.noDirection);
        ADD_CHAR(("!"), charLinkAndDirection.noDirection);
        ADD_CHAR(("~"), charLinkAndDirection.noDirection);
        ADD_CHAR(("\\"), charLinkAndDirection.noDirection);
        ADD_CHAR(("\""), charLinkAndDirection.noDirection);

        //////// math
        ADD_CHAR(("+"), charLinkAndDirection.noDirection);
        ADD_CHAR(("-"), charLinkAndDirection.noDirection);
        ADD_CHAR(("*"), charLinkAndDirection.noDirection);
        ADD_CHAR(("/"), charLinkAndDirection.noDirection);
        ADD_CHAR(("="), charLinkAndDirection.noDirection);

        addNewChar(("ـ"), 'ـ', 'ـ', 'ـ', 'ـ', charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked);
    }
    private void ADD_CHAR(string schar, charLinkAndDirection direction)
	{
		if (uniindex != 0)
		{
            if (comparEnum(direction, charLinkAndDirection.RightLinked) && comparEnum(direction , charLinkAndDirection.LeftLinked))
			{
				addNewChar(schar, uniindex, uniindex + 1, uniindex + 2, uniindex + 3, direction);
				uniindex += 4;
			}
			else
            if (comparEnum( direction , charLinkAndDirection.RightLinked ))
			{
				addNewChar(schar, uniindex, uniindex + 1, 0, 0, direction);
				uniindex += 2;
			}
			else
			{
			addNewChar(schar, uniindex, 0, 0, 0, direction);
			uniindex++;
			}
		}
		else
		addNewChar(schar, 0, 0, 0, 0, direction);

	}
	private int addNewChar(string schar, uint nchar, uint rchar, uint lchar, uint rlchar, charLinkAndDirection direction)
	{
		arCharSet cs = new arCharSet();
		cs.Direction = direction;
		cs.sChar = schar;
        cs.nChar = ((char)nchar).ToString();
		cs.rChar = ((char)rchar).ToString();
		cs.lChar = ((char)lchar).ToString();
		cs.rlChar = ((char)rlchar).ToString();
		cs.charSize = schar.Length;
		mCharSet.Add(cs);
		return (mCharSet.Count - 1);
	}
	private charLinkAndDirection checkNextChar(string in_String, int n, int sspos, charLinkAndDirection ignoreType)
	{
		bool noChar = true;
		while (sspos <= n)
		{
			noChar = true;
			for (int i = 0;i < mCharSet.Count;i++)
			{
				if (dStrCmp(in_String, i, sspos))
				{
					 if (comparEnum(mCharSet[i].Direction , ignoreType ))
					 {
							sspos += mCharSet[i].charSize;
							noChar = false;
							break;
					 }
					return (mCharSet[i].Direction);
				}
			}
			if (noChar)
				break;
		}
	return (0);
	}
	private int checknoDirectionLen(string in_String, int n, int sspos)
	{
		int len = 0;
        arCharSet  c = getChar(in_String, n, sspos + len);
		while (c != null)
		{
            if (!comparEnum(c.Direction , charLinkAndDirection.noDirection))
				return len;
			len += c.charSize;
            c = getChar(in_String, n, sspos + len);
		}
		return len;
	}
	private bool checkNextCharDirection(string in_String, int n, int sspos)
	{
        arCharSet c = getChar(in_String, n, sspos);

        while (c != null)
        {
			if ((c.Direction & charLinkAndDirection.noDirection) != charLinkAndDirection.noDirection)
				return true;
			sspos += c.charSize;
            c = getChar(in_String, n, sspos);
		}
	return (sspos >= n);
	}
	private CharSetControl.arCharSet getChar(string in_String, int n, int sspos)
	{
		for (int i = 0;i < mCharSet.Count;i++)
		{
			if (dStrCmp(in_String, i, sspos))
			{
				return mCharSet[i];
			}
		}
		return null;
	}
	private bool checkFirstChar(string in_String, int n, int sspos, ref arCharSet frstChar, ref int frstCharIndex)
	{
		frstCharIndex = n;
		for (int i = 0;i < mCharSet.Count;i++)
		{
			int k = (dStrpos(in_String, i, sspos, n));
			if (k > -1)
			{
			 if (k < frstCharIndex)
			 {
				frstChar = mCharSet[i];
				frstCharIndex = k;
			 }
			}
		}
	return (frstCharIndex != n);
	}

    static public float getCharPos(csRes StrIndexs, int charPos)
    {
        return _control.iGetCharPos(StrIndexs, charPos);
    }
    private float iGetCharPos(csRes StrIndexs, int charPos)
    {
		float sspos = 0F;
		float d;
		int frstChar = 0;
		int lastChar = 0;
		float frstSspos = 0F;
		float lastSspos = 0F;
		int i = 0;
		for (i = 0;i < StrIndexs.strIndexs.Count;i++)
		{
			if ((charPos > StrIndexs.strIndexs[i].strPos) && (charPos <= StrIndexs.strIndexs[i].strPos + StrIndexs.strIndexs[i].orginalSize))
			{
				d = StrIndexs.strIndexs[i].leftToRight ? (1.0f * (charPos - StrIndexs.strIndexs[i].strPos) * StrIndexs.strIndexs[i].strSize / StrIndexs.strIndexs[i].orginalSize) : 1.0f - (1.0f * (charPos - StrIndexs.strIndexs[i].strPos) * StrIndexs.strIndexs[i].strSize / StrIndexs.strIndexs[i].orginalSize);
				return (sspos + d);
			}
			if (StrIndexs.strIndexs[frstChar].strPos > StrIndexs.strIndexs[i].strPos)
			{
				frstChar = i;
				frstSspos = sspos;
			}
			if (StrIndexs.strIndexs[lastChar].strPos < StrIndexs.strIndexs[i].strPos)
			{
				lastChar = i;
				lastSspos = sspos;
			}
			sspos += StrIndexs.strIndexs[i].strSize;
		}
		if (charPos > 1)
		{
			i = lastChar;
			sspos = lastSspos;
			d = StrIndexs.strIndexs[i].leftToRight ? StrIndexs.strIndexs[i].strSize : 0;
		}
		else
		{
			i = frstChar;
			sspos = frstSspos;
			d = StrIndexs.strIndexs[i].leftToRight ? 0 : StrIndexs.strIndexs[i].strSize;
		}
		return (sspos + d);
	}
    private void getCharsPoss(ref csRes out_string)
    {
         float sspos = 0;
         bool[] assignedP = new bool[out_string.charPlaces.Length];
         for (int i = 0; i < assignedP.Length; i++)
             assignedP[i] = false;
         for (int i = 0; i < out_string.strIndexs.Count; i++)
         {
             for (int j = 0; j < out_string.strIndexs[i].orginalSize; j++)
             {
                 float d = out_string.strIndexs[i].leftToRight ? (1.0f * j * out_string.strIndexs[i].strSize / out_string.strIndexs[i].orginalSize) : 1.0f - (1.0f * j * out_string.strIndexs[i].strSize / out_string.strIndexs[i].orginalSize);
                 int id = out_string.strIndexs[i].strPos + j;
                 if (id>=assignedP.Length)
				  {
//					Debug.Log(id+"    "+assignedP.Length+ "   "+out_string.orginalStr.Length+"   "+out_string.strIndexs.Count);
					continue;
				  }
                 if (assignedP[id])
                     continue;
                 assignedP[id] = true;
                 out_string.charPlaces[id] = sspos + d;
             }
             sspos += out_string.strIndexs[i].strSize;
         }
    }
    private float posToCharIndex(List<strIndex> StrIndexs, float pos, bool rightToLeft)
	{
		float sspos = 0F;
		float d;
		int frstChar = 0;
		int lastChar = 0;
		float frstSspos = 0F;
		float lastSspos = 0F;
		int i = 0;
		if ((pos == 0) && (StrIndexs.Count != 0))
			return StrIndexs[0].strPos + (StrIndexs[0].leftToRight ? 0 : StrIndexs[0].orginalSize);
		for (i = 0;i < StrIndexs.Count;i++)
		{
			int charStp = StrIndexs[i].strSize; //StrIndexs[i].leftToRight ? (StrIndexs[i].strSize) : 0 ;
			if ((pos > sspos) && (pos <= sspos + charStp))
			{
				d = 1.0f * ((pos - sspos) * StrIndexs[i].orginalSize / StrIndexs[i].strSize);
				return (StrIndexs[i].strPos + d);
			}

			if (StrIndexs[frstChar].strPos > StrIndexs[i].strPos)
			{
				frstChar = i;
				frstSspos = sspos;
			}
			if (StrIndexs[lastChar].strPos < StrIndexs[i].strPos)
			{
				lastChar = i;
				lastSspos = sspos;
			}
			sspos += StrIndexs[i].strSize;
		}
		if (pos > 1)
		{
			i = lastChar;
			sspos = lastSspos;
			d = StrIndexs[i].leftToRight ? StrIndexs[i].orginalSize : 0;
		}
		else
		{
			i = frstChar;
			sspos = frstSspos;
			d = StrIndexs[i].leftToRight ? 0 : StrIndexs[i].orginalSize;
		}
		return (sspos + d);
	}
    private void charsPositions(List<strIndex> StrIndexs, int num, ref float[] cPos, bool rightToLeft)
	{
		float sspos = 0F;
		float d;
		int i = 0;
        cPos = new float[num];
		for (i = 0;i < StrIndexs.Count;i++)
		{
			for (int j=0;j<StrIndexs[i].orginalSize;j++)
			{
				d = 1.0f * ( j / StrIndexs[i].strSize);
				cPos[StrIndexs[i].strPos+j] = sspos + d;
			}
			sspos += StrIndexs[i].strSize;
		}
	}
	
    /*static public void fixArabicSymbols(string in_String, ref string out_String)
    {
		string istr = in_String;
		string ostr = "";
		out_String  = "";
		int p = istr.IndexOf(nlChar);
		while (p != -1)
		{
			string sstr = istr.Substring( 0, p);
			istr = istr.Substring( p, istr.Length - p).Trim();
	        _control.iFixArabicSymbols(sstr, ref ostr, false, true);
			out_String += ostr +nlChar;
			p = istr.IndexOf(nlChar);
		}
        _control.iFixArabicSymbols(istr, ref ostr, false, true);
		out_String += ostr;
    }*/
	
	
    static public string getUnfixed(string in_String)
    {
        return _control.iGetUnfixed(in_String);
    }

	string iGetUnfixed(string in_String)
    {
    	foreach(string key in arabicTable.Keys)
		{
			if (arabicTable[key].convStr.Equals(in_String))
			{
				return key;
			}
		}
		Debug.Log("["+in_String+"]key not found for unfixing!");
		return in_String;
	}

	static public string fixArabicSymbols(string in_String)
	{
		string result = string.Empty;
		_control.iFixArabicSymbols(in_String, ref result, false, true);
		return result;
	}
	
    static public void fixArabicSymbols(string in_String, ref string out_String)
    {
        _control.iFixArabicSymbols(in_String, ref out_String, false, true);
    }
    static public void fixArabicSymbols(string in_String, ref string out_String, bool rightToLeft)
    {
        _control.iFixArabicSymbols(in_String, ref out_String, false, rightToLeft);
    }
    static public void fixArabicSymbols(string in_String, ref string out_String, bool charsIndexs, bool rightToLeft)
    {
        _control.iFixArabicSymbols(in_String, ref out_String, charsIndexs, rightToLeft);
    }
    private void iFixArabicSymbols(string in_String, ref string out_String, bool charsIndexs, bool rightToLeft)
    {
        if (arabicTable.ContainsKey(in_String))
        {
            out_String = arabicTable[in_String].convStr;
            return;
        }
        csRes out_Res = new csRes();
        iFixArabicSymbols(in_String, ref out_Res, charsIndexs, rightToLeft);
        out_String = out_Res.convStr;
    }
    static public void fixArabicSymbols(string in_String, ref csRes out_String)
    {
        _control.iFixArabicSymbols(in_String, ref out_String, false, true);
    }
    static public void fixArabicSymbols(string in_String, ref csRes out_String, bool rightToLeft)
    {
        _control.iFixArabicSymbols(in_String, ref out_String, false, rightToLeft);
    }
    static public void fixArabicSymbols(string in_String, ref csRes out_String, bool charsIndexs, bool rightToLeft)
    {
    	_control.iFixArabicSymbols(in_String, ref out_String, charsIndexs, rightToLeft);
    }
    private void iFixArabicSymbolsML(string in_String, ref csRes out_String, bool charsIndexs, bool rightToLeft)
    {
        /*if (arabicTable.ContainsKey(in_String))
        {
            out_String = arabicTable[in_String];
            return;
        }*/
		string istr = in_String;
		int p = istr.IndexOf(nlChar);
		csRes oString;
		out_String.strIndexs = new List<strIndex>();
		
		while (p != -1 && istr != "")
		{
			int nlpos = in_String.Length-istr.Length;
			string sstr = istr.Substring( 0, p);
			istr = istr.Substring( p, istr.Length - p);
			oString = new csRes();
        	parsingSegments(sstr, ref oString, rightToLeft);
	        for (int i = 0; i < oString.strIndexs.Count; i++)
	        {
				strIndex ostr = oString.strIndexs[i];
	            ostr.strPos += nlpos;
				out_String.strIndexs.Add(ostr);
	        }
			while(istr.IndexOf(nlChar)==0 && istr != "")
			{
				strIndex nl = new strIndex();
				nl.leftToRight = false; nl.orginalSize = 1; nl.str = nlChar.ToString(); nl.strSize =1; nl.strPos = in_String.Length-istr.Length;
				out_String.strIndexs.Add(nl);
				istr = istr.Substring( 1, istr.Length - 1);
			}
			p = istr.IndexOf(nlChar);
		}
		oString = new csRes();
        parsingSegments(istr, ref oString, rightToLeft);
        for (int i = 0; i < oString.strIndexs.Count; i++)
        {
			strIndex ostr = oString.strIndexs[i];
            ostr.strPos += in_String.Length-istr.Length;
			out_String.strIndexs.Add(ostr);
        }
		
        int sspos = 0;
        out_String.convStr = "";
        if (out_String.strIndexs.Count == 0)
            return;
        for (int i = 0; i < out_String.strIndexs.Count; i++)
        {
            out_String.convStr += out_String.strIndexs[i].str;
            sspos += out_String.strIndexs[i].strSize;
        }
        out_String.orginalStr = in_String;
        out_String.charPlaces = new float[in_String.Length];
        getCharsPoss(ref out_String);
		/*string str = "";
		for(int i=0;i<in_String.Length;i++)
		{
			str += "["+i+"  "+out_String.charPlaces[i]+"]";
		}
		Debug.Log(str);*/
        //arabicTable.Add(in_String, out_String);
    }
    private void iFixArabicSymbols(string in_String, ref csRes out_String, bool charsIndexs, bool rightToLeft)
    {
		if (in_String != null)
		if (in_String.IndexOf(nlChar) != -1)
		{
        	iFixArabicSymbolsML(in_String, ref out_String, charsIndexs, rightToLeft);
        	arabicTable.Add(in_String, out_String);
			return;
		}
        if (arabicTable.ContainsKey(in_String))
        {
            out_String = arabicTable[in_String];
            return;
        }
        parsingSegments(in_String, ref out_String, rightToLeft);
        int sspos = 0;
        out_String.convStr = "";
        if (out_String.strIndexs.Count == 0)
            return;
        for (int i = 0; i < out_String.strIndexs.Count; i++)
        {
            out_String.convStr += out_String.strIndexs[i].str;
            sspos += out_String.strIndexs[i].strSize;
        }
        out_String.orginalStr = in_String;
        out_String.charPlaces = new float[in_String.Length];
        getCharsPoss(ref out_String);
        arabicTable.Add(in_String, out_String);
    }
    private void parsingSegments(string in_String, ref csRes StrIndexs)
	{
		parsingSegments(in_String, ref StrIndexs, false);
	}
	
	bool englishOnly(string in_String)
	{
		string ret = in_String;
        int n = in_String.Length;
		int sspos = 0;
		int k = 0;
		arCharSet currentChar = null;
		while ( checkFirstChar(ret, n, sspos, ref currentChar, ref k))
		{
                if (!((currentChar.Direction == charLinkAndDirection.WidthLess) || (comparEnum(currentChar.Direction , charLinkAndDirection.noDirection))))
					return false;
				sspos = k+ currentChar.charSize;
		}
		return true;
	}
    private void parsingSegments(string in_String, ref csRes StrIndexs, bool rightToLeft)
	{
		strIndex nPharse = new strIndex();
        StrIndexs.strIndexs = new List<strIndex>();
		//if (in_String == "")
			//return;
		if (englishOnly(in_String))
		{
			nPharse.str = in_String;
			nPharse.strPos = 0;
			nPharse.leftToRight = true;
			nPharse.strSize = in_String.Length;
			nPharse.orginalSize = in_String.Length;
			StrIndexs.strIndexs.Add(nPharse);
			return;
		}
		string ret = in_String;

		charLinkAndDirection laststate = charLinkAndDirection.NotLinkable;
        charLinkAndDirection CrntState = charLinkAndDirection.noDirection;
        charLinkAndDirection nextstate = charLinkAndDirection.noDirection;
        int n = in_String.Length;
		int sspos = 0;
		int k = 0;
		int newWordPos = 0;
		bool rDirection = false;
		arCharSet currentChar = null;
		int EngWords = 0;
		int noDirectWords = -1;
		while ( checkFirstChar(ret, n, sspos, ref currentChar, ref k))
		{
		  if (k > sspos)
			  laststate = 0;
                charLinkAndDirection reqDir = ((comparEnum(currentChar.Direction , charLinkAndDirection.noDirection)) ? charLinkAndDirection.noDirection : charLinkAndDirection.NotLinkable) | charLinkAndDirection.WidthLess;
		        nextstate = checkNextChar(ret, n, k + currentChar.charSize, reqDir);
				nextstate = (nextstate & charLinkAndDirection.RightLinked);
                if ((currentChar.Direction == charLinkAndDirection.WidthLess) || (comparEnum(currentChar.Direction , charLinkAndDirection.noDirection)))
				{
						CrntState = currentChar.Direction;
				}
				else
                    CrntState = (comparEnum(nextstate, charLinkAndDirection.RightLinked) ? (charLinkAndDirection.LeftLinked & currentChar.Direction) : charLinkAndDirection.NotLinkable) |
                                    (comparEnum(laststate, charLinkAndDirection.LeftLinked) ? (charLinkAndDirection.RightLinked & currentChar.Direction) : charLinkAndDirection.NotLinkable);
					int noArabLen = k - sspos;
					if (noArabLen > 0 )
					{
                        nPharse.str = in_String.Substring(sspos, noArabLen);
                        nPharse.strPos = sspos;
						nPharse.leftToRight = true;
						nPharse.strSize = noArabLen;
						nPharse.orginalSize = noArabLen;

						if (rightToLeft == rDirection)
                            StrIndexs.strIndexs.Insert(0, nPharse);
						else
                            StrIndexs.strIndexs.Insert(EngWords, nPharse);
						newWordPos = EngWords + 1; //StrIndexs.size();
						rDirection = false;
						sspos = k;
						EngWords++;
					}
					else
						EngWords = 0;
					if (comparEnum(CrntState , charLinkAndDirection.noDirection))
						noDirectWords++;
					else
						noDirectWords = -1;
					if (((CrntState & charLinkAndDirection.noDirection) != charLinkAndDirection.noDirection) || (nextstate != 0 && rightToLeft))
						rDirection = true;
					if ((rDirection != rightToLeft) && (EngWords != 0))
					{
						//	newWordPos = EngWords;//StrIndexs.size();
						//	EngWords++;
					}
					else
							newWordPos = 0;
					switch (CrntState)
					{
						case charLinkAndDirection.NotLinkable :
							 nPharse.str = currentChar.nChar;
							 nPharse.strPos = sspos;
							 nPharse.leftToRight = false;
							 nPharse.strSize = 1;
							 nPharse.orginalSize = currentChar.charSize;
							 break;
						case charLinkAndDirection.RightLinked :
							nPharse.str = currentChar.rChar;
							nPharse.strPos = sspos;
							nPharse.leftToRight = false;
							nPharse.strSize = 1;
							nPharse.orginalSize = currentChar.charSize;
							 break;
						case charLinkAndDirection.LeftLinked :
                             nPharse.str = currentChar.lChar;
							nPharse.strPos = sspos;
							nPharse.leftToRight = false;
							nPharse.strSize = 1;
							nPharse.orginalSize = currentChar.charSize;
							 break;
						case (charLinkAndDirection.RightLinked | charLinkAndDirection.LeftLinked) :
							nPharse.str = currentChar.rlChar;
							nPharse.strPos = sspos;
							nPharse.leftToRight = false;
							nPharse.strSize = 1;
							nPharse.orginalSize = currentChar.charSize;
							 break;
						case charLinkAndDirection.WidthLess :
							nPharse.str = currentChar.sChar;
							nPharse.strPos = sspos;
							nPharse.leftToRight = false;
							nPharse.strSize = 1;
							nPharse.orginalSize = currentChar.charSize;
							 break;
						case charLinkAndDirection.noDirection :
						case charLinkAndDirection.noDirection | charLinkAndDirection.numbers :
							int ndLen = checknoDirectionLen(ret, n, k);
							if ((nextstate == 0 && rightToLeft) && (sspos > n))
								newWordPos = 0;
							int numOfLeft = -1;
							for (int i = 0;i < ndLen;i++)
							{
								checkFirstChar(ret, n, sspos, ref currentChar, ref k);
                                if (rightToLeft && currentChar.nChar != "\0" && rDirection)
									nPharse.str = currentChar.nChar;
								else
									nPharse.str = currentChar.sChar;
								nPharse.strPos = k;
								nPharse.leftToRight = comparEnum(currentChar.Direction , charLinkAndDirection.numbers) ? true :!(rightToLeft && rDirection);
								nPharse.strSize = 1;
								nPharse.orginalSize = 1;
								sspos = k + 1;
								if (nPharse.leftToRight)
									numOfLeft++;
								else
									numOfLeft = -1;
								newWordPos = (!nPharse.leftToRight ? 0 : EngWords + numOfLeft);
								if (i < ndLen - 1)
                                    StrIndexs.strIndexs.Insert(newWordPos, nPharse);
							}
							if (EngWords != 0)
								EngWords += ndLen;
							break;

					}
                    StrIndexs.strIndexs.Insert(newWordPos, nPharse);

					if ((CrntState & charLinkAndDirection.noDirection) != charLinkAndDirection.noDirection)
						sspos = k + currentChar.charSize;
					if (currentChar.Direction != charLinkAndDirection.WidthLess)
						laststate = (currentChar.Direction & charLinkAndDirection.LeftLinked);
				if (sspos > n)
					break;
		}
        if (StrIndexs.strIndexs.Count != 0)
		{
					if (sspos < n)
					{
                        nPharse.str = in_String.Substring(sspos, n-sspos);
                        nPharse.strPos = sspos;
						nPharse.leftToRight = true;
						nPharse.strSize = n - sspos;
						nPharse.orginalSize = nPharse.strSize;
						if (rightToLeft == rDirection)
                            StrIndexs.strIndexs.Insert(0, nPharse);
						else
                            StrIndexs.strIndexs.Insert(EngWords, nPharse);
					}
		}
		else
		{
			nPharse.str = in_String;
			nPharse.strPos = 0;
			nPharse.leftToRight = true;
			nPharse.strSize = n;
			nPharse.orginalSize = nPharse.strSize;
            StrIndexs.strIndexs.Add(nPharse);

		}
	}

    static public int posToCharIndex(int textWidth, string in_string, csRes in_string2,int p)
    {
        return _control.iPosToCharIndex(textWidth, in_string, in_string2, p);
    }
    private int iPosToCharIndex(int textWidth, string in_string, csRes in_string2,int p)
    {
        if (textWidth <= 0)
            return 0;
        int len = in_string2.convStr.Length;
        int pos = Math.Max(0, Math.Min(len - 1, (int)Math.Round(1.0f * p * len / textWidth)));
        for (int i = 0; i < in_string2.charPlaces.Length; i++)
        {
            if (in_string2.charPlaces[i] == pos)
            {
                pos = i;
                break;
            }
        }
        return pos;
    }

    static public int posToCharPos(int textWidth, string in_string1, csRes in_string, int pos)
    {
        return _control.iPosToCharPos(textWidth, in_string1, in_string, pos);
    }

    private int iPosToCharPos(int textWidth, string in_string1, csRes in_string, int pos)
    {
		if (in_string1 == null)
			return 0;
		if (in_string1.Length <= 1)
			return 0;
        int cPos = iPosToCharIndex(textWidth, in_string1, in_string, pos);
        if (cPos >= in_string.charPlaces.Length)
			return 0;
        cPos = (int)in_string.charPlaces[cPos];
        return (cPos) * textWidth / (in_string1.Length - 1); 

    }
	private bool dStrNext(string in_string1, int sspos)
	{
		var nextChar = getChar( in_string1, in_string1.Length, sspos + mCharSet[0].charSize);
		if (nextChar == null)
			return false;
		return comparEnum( nextChar.Direction , charLinkAndDirection.RightLinked) ;	
	}
	private int dStrpos(string in_string1, int i, int sspos, int len1)
    {
		int len2 = mCharSet[i].charSize;
        if (sspos + len2 > len1)
            return -1;
		int res = in_string1.IndexOf(mCharSet[i].sChar, sspos, StringComparison.Ordinal);
		if (res != -1 && i == 0)
		if (dStrNext (in_string1, sspos))
			return -1;
		return res;
    }
	private bool dStrCmp(string in_string1, int i, int sspos)
	{
		bool res =  sspos == in_string1.IndexOf(mCharSet[i].sChar, sspos, StringComparison.Ordinal);
		if (res && i == 0)
		if (dStrNext (in_string1, sspos))
			return false;
		return res;
	}
}