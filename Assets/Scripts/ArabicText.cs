using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
namespace UI
{
	public class ArabicText : Text
	{
		protected override void OnEnable()
		{
			base.OnEnable();
		}
		protected override void OnDisable()
		{
			base.OnDisable();
		}

        private readonly UIVertex[] m_TempVerts = new UIVertex[4];
        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            if (font == null)
            {
                return;
            }

			if (!m_Text.IsArabic())
			{
				base.OnPopulateMesh( toFill);
				return;
			}

            m_DisableFontTextureRebuiltCallback = true;
            Vector2 size = base.rectTransform.rect.size;
            TextGenerationSettings generationSettings = GetGenerationSettings(size);

			string[] words = m_Text.Split(' ');
			for (int i = 0; i < words.Length; i++)
				words[i] = words[i].ToArabic();
			string arabicStr1 = string.Join(" ", words);
			cachedTextGenerator.Populate(arabicStr1, generationSettings);
            string arabicStr = "";
			int StrStart = 0, StrEnd = arabicStr1.Length;
            for (int i = cachedTextGenerator.lineCount - 1; i >= 0; i--)
            {
                StrStart = cachedTextGenerator.lines[i].startCharIdx;
				arabicStr = arabicStr1.Substring(StrStart, StrEnd - StrStart).FlipWords() + (arabicStr.NotNullOrEmpty() ? "\n" + arabicStr : "");
                StrEnd = StrStart;
            }
            cachedTextGenerator.Populate(arabicStr, generationSettings);

            Rect rect = base.rectTransform.rect;
            Vector2 textAnchorPivot = Text.GetTextAnchorPivot(alignment);
            Vector2 zero = Vector2.zero;
            zero.x = ((textAnchorPivot.x != 1f) ? rect.xMin : rect.xMax);
            zero.y = ((textAnchorPivot.y != 0f) ? rect.yMax : rect.yMin);
            Vector2 vector = base.PixelAdjustPoint(zero) - zero;
            IList<UIVertex> verts = cachedTextGenerator.verts;
            float num = 1f / pixelsPerUnit;
            int num2 = verts.Count - 4;
            toFill.Clear();
            if (vector != Vector2.zero)
            {
                for (int i = 0; i < num2; i++)
                {
                    int num3 = i & 3;
                    m_TempVerts[num3] = verts[i];
                    UIVertex[] expr_147_cp_0 = m_TempVerts;
                    int expr_147_cp_1 = num3;
                    expr_147_cp_0[expr_147_cp_1].position = expr_147_cp_0[expr_147_cp_1].position * num;
                    UIVertex[] expr_16B_cp_0_cp_0 = m_TempVerts;
                    int expr_16B_cp_0_cp_1 = num3;
                    expr_16B_cp_0_cp_0[expr_16B_cp_0_cp_1].position.x = expr_16B_cp_0_cp_0[expr_16B_cp_0_cp_1].position.x + vector.x;
                    UIVertex[] expr_190_cp_0_cp_0 = m_TempVerts;
                    int expr_190_cp_0_cp_1 = num3;
                    expr_190_cp_0_cp_0[expr_190_cp_0_cp_1].position.y = expr_190_cp_0_cp_0[expr_190_cp_0_cp_1].position.y + vector.y;
                    if (num3 == 3)
                    {
                        toFill.AddUIVertexQuad(m_TempVerts);
                    }
                }
            }
            else
            {
                for (int j = 0; j < num2; j++)
                {
                    int num4 = j & 3;
                    m_TempVerts[num4] = verts[j];
                    UIVertex[] expr_201_cp_0 = m_TempVerts;
                    int expr_201_cp_1 = num4;
                    expr_201_cp_0[expr_201_cp_1].position = expr_201_cp_0[expr_201_cp_1].position * num;
                    if (num4 == 3)
                    {
                        toFill.AddUIVertexQuad(m_TempVerts);
                    }
                }
            }
            m_DisableFontTextureRebuiltCallback = false;

        }
	}
}
