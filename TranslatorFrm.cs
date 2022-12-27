using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic;

namespace ChartHelper
{
    public partial class TranslatorFrm : Form
    {
        public TranslatorFrm()
        {
            InitializeComponent();
        }

        string DbPath = "DbPath.txt";

        private static readonly char[] IniC = { 'ㄱ', 'ㄲ', 'ㄴ', 'ㄷ', 'ㄸ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅃ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅉ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };
        private static readonly string[] IniS = { "ㄱ", "ㄲ", "ㄴ", "ㄷ", "ㄸ", "ㄹ", "ㅁ", "ㅂ", "ㅃ", "ㅅ", "ㅆ", "ㅇ", "ㅈ", "ㅉ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ" };

        private static readonly char[] VolC = { 'ㅏ', 'ㅐ', 'ㅑ', 'ㅒ', 'ㅓ', 'ㅔ', 'ㅕ', 'ㅖ', 'ㅗ', 'ㅘ', 'ㅙ', 'ㅚ', 'ㅛ', 'ㅜ', 'ㅝ', 'ㅞ', 'ㅟ', 'ㅠ', 'ㅡ', 'ㅢ', 'ㅣ' };
        private static readonly string[] VolS = { "ㅏ", "ㅐ", "ㅑ", "ㅒ", "ㅓ", "ㅔ", "ㅕ", "ㅖ", "ㅗ", "ㅘ", "ㅙ", "ㅚ", "ㅛ", "ㅜ", "ㅝ", "ㅞ", "ㅟ", "ㅠ", "ㅡ", "ㅢ", "ㅣ" };

        private static readonly char[] UndC = { '\0', 'ㄱ', 'ㄲ', 'ㄳ', 'ㄴ', 'ㄵ', 'ㄶ', 'ㄷ', 'ㄹ', 'ㄺ', 'ㄻ', 'ㄼ', 'ㄽ', 'ㄾ', 'ㄿ', 'ㅀ', 'ㅁ', 'ㅂ', 'ㅄ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };
        private static readonly string[] UndS = { "", "ㄱ", "ㄲ", "ㄳ", "ㄴ", "ㄵ", "ㄶ", "ㄷ", "ㄹ", "ㄺ", "ㄻ", "ㄼ", "ㄽ", "ㄾ", "ㄿ", "ㅀ", "ㅁ", "ㅂ", "ㅄ", "ㅅ", "ㅆ", "ㅇ", "ㅈ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ" };

        private static readonly string[] Table =
        {
            "ㄱ", "r", "ㄲ", "R",  "ㄳ", "rt",
            "ㄴ", "s", "ㄵ", "sw", "ㄶ", "sg",
            "ㄷ", "e", "ㄸ", "E",
            "ㄹ", "f", "ㄺ", "fr", "ㄻ", "fa", "ㄼ", "fq", "ㄽ", "ft", "ㄾ", "fx", "ㄿ", "fv", "ㅀ", "fg",
            "ㅁ", "a",
            "ㅂ", "q", "ㅃ", "Q",  "ㅄ", "qt",
            "ㅅ", "t", "ㅆ", "T",
            "ㅇ", "d",
            "ㅈ", "w",
            "ㅉ", "W",
            "ㅊ", "c",
            "ㅋ", "z",
            "ㅌ", "x",
            "ㅍ", "v",
            "ㅎ", "g",
            "ㅏ", "k",
            "ㅐ", "o", "ㅒ", "O",
            "ㅑ", "i",
            "ㅓ", "j",
            "ㅔ", "p", "ㅖ", "P",
            "ㅕ", "u",
            "ㅗ", "h", "ㅘ", "hk", "ㅙ", "ho", "ㅚ", "hl",
            "ㅛ", "y",
            "ㅜ", "n", "ㅝ", "nj", "ㅞ", "np", "ㅟ", "nl",
            "ㅠ", "b",
            "ㅣ", "l",
            "ㅡ", "m", "ㅢ", "ml",
        };

        private static char GetKor(string src, int index, int type, out int len, bool onlyOne = false)
        {
            len = 0;
            if (index >= src.Length) return '\0';

            int i = -1;

            if (type != 0 && !onlyOne && index + 1 < src.Length)
            {
                i = Array.IndexOf<string>(Table, new string(new char[] { src[index], src[index + 1] }));
                len = 2;
            }

            if (i == -1)
            {
                i = Array.IndexOf<string>(Table, src[index].ToString());
                len = 1;
            }

            var c = i >= 0 ? Table[i - 1][0] : '\0';

            if (type == 0) return Array.IndexOf<char>(IniC, c) >= 0 ? c : '\0';
            if (type == 1) return Array.IndexOf<char>(VolC, c) >= 0 ? c : '\0';
            if (type == 2) return Array.IndexOf<char>(UndC, c) >= 0 ? c : '\0';

            len = 0;
            return '\0';
        }

        private static bool Split(char src, out int ini, out int vow, out int und)
        {
            // 원래 초중종 나눔
            int charCode = Convert.ToInt32(src) - 44032;
            int i;

            if ((charCode < 0) || (charCode > 11171))
            {
                ini = vow = und = -1;

                if ((i = Array.IndexOf<char>(IniC, src)) != -1)
                    ini = i;
                else if ((i = Array.IndexOf<char>(VolC, src)) != -1)
                    vow = i;
                else if (src != '\0' && (i = Array.IndexOf<char>(UndC, src)) != -1)
                    und = i;
            }
            else
            {
                ini = charCode / 588;
                vow = (charCode % 588) / 28;
                und = (charCode % 588) % 28;
            }

            return ini != -1 || vow != -1 || und != -1;
        }
        private static char Combine(char ini, char vow, char und = '\0')
        {
            // 조합
            int i = 44032 + Array.IndexOf<char>(IniC, ini) * 588;
            if (vow != '\0') i += Array.IndexOf<char>(VolC, vow) * 28;
            if (und != '\0') i += Array.IndexOf<char>(UndC, und);

            return Convert.ToChar(i);
        }

        /// <summary>
        /// gksrmfdl dkscuwudy -> 한글이 안쳐져요
        /// </summary>
        /// <param name="eng">변환할 문자열입니다.</param>
        /// <param name="detectCase">대문자가 더 많은 경우에 대소문자를 반대로 바꿔서 변환합니다</param>
        /// <returns>변환된 결과값입니다</returns>
        /// <exception cref="ArgumentNullException">eng 가 Null 일때 발생합니다</exception>
        /// <exception cref="ArgumentException">eng 의 길이가 0 일때 발생합니다</exception>
        public static string Eng2Kor(string eng, bool detectCase = false)
        {
            if (eng == null) throw new ArgumentNullException();
            if (eng.Length == 0) throw new ArgumentException();

            int index = 0;
            var b = new StringBuilder(eng.Length);

            // 대문자가 더 많으면 대소문자 반대로 바꿈
            if (detectCase)
            {
                b.Append(eng);

                int low = 0, up = 0;
                for (index = 0; index < b.Length; ++index)
                {
                    if (char.IsUpper(b[index])) up++;
                    else if (char.IsLower(b[index])) low++;
                }

                if (up > low)
                {
                    for (index = 0; index < b.Length; ++index)
                    {
                        if (char.IsUpper(b[index])) b[index] = char.ToLower(b[index]);
                        else if (char.IsLower(b[index])) b[index] = char.ToUpper(b[index]);
                    }

                    eng = b.ToString();

                    b.Remove(0, b.Length);
                }
            }

            char ini, vow, und, tmp;
            int len, len2;

            index = 0;
            while (index < eng.Length)
            {
                ini = vow = und = '\0';

                ////////////////////////////////////////////////// 초성
                ini = GetKor(eng, index, 0, out len);

                // 초성이 아니면
                if (ini == '\0')
                {
                    // 자음이 아니면 모음이냐?
                    vow = GetKor(eng, index, 1, out len);

                    // 모음도 아니네 :3
                    if (vow == '\0')
                    {
                        b.Append(eng[index]);
                        index++;
                        continue;
                    }

                    // 모음 맞네!!!
                    b.Append(vow);
                    index += len;
                    continue;
                }

                // 모음 다음에 모음이면... 조합 모음?
                if (GetKor(eng, index + 1, 0, out len2) != '\0')
                {
                    // 근데 자자모 순서대로면 조합 모음이 아니라 단순한 모음이니까
                    // ㄱㄱㅏ -> ㄱ가
                    if (GetKor(eng, index + 2, 1, out len2) != '\0')
                    {
                        b.Append(ini);
                        index += len;
                        continue;
                    }

                    // 조합 모음이 맞는지 확인
                    und = GetKor(eng, index, 2, out len2);

                    if (len2 == 2)
                    {
                        // 조합 모음이 맞네
                        b.Append(und);
                        index += len2;
                        continue;
                    }

                    // 시무룩. 조합모음이 아니였다.
                    // 집어넣고 다음 기회를 노리자
                    else
                    {
                        b.Append(ini);
                        index += len;
                        continue;
                    }
                }

                // 초성 길이만큼 이동. 어처피 한글자임
                index += 1;

                ////////////////////////////////////////////////// 중성
                vow = GetKor(eng, index, 1, out len);

                // 중성이 아니면 초성만 넣고 스킵
                if (vow == '\0')
                {
                    b.Append(ini);
                    continue;
                }

                // 중성 길이만큼 이동
                index += len;

                ////////////////////////////////////////////////// 종성
                und = GetKor(eng, index, 2, out len);
                // 종성이 아니면 조합해서 넣고 다음으로.
                if (und == '\0')
                {
                    b.Append(Combine(ini, vow));
                    continue;
                }

                // 자음 뒤에 모음이 나오는 경우 대비
                // 예) 각시 ㄱㅏㄱㅅㅣ => 갃X
                if (len == 2)
                {
                    // 자모자자자 순서면 이게 조합 모음이 맞음.
                    if (GetKor(eng, index + 2, 0, out len2) != '\0')
                    {
                        b.Append(Combine(ini, vow, und));
                        index += len;
                        continue;
                    }

                    // 어이쿠 조합 모음이 아니라 그냥 모음이였네요.
                    und = GetKor(eng, index, 2, out len, true);

                    b.Append(Combine(ini, vow, und));
                    index += len;
                    continue;
                }
                // 가시 = ㄱㅏㅅㅣ ->갓X
                else
                {
                    // 다음에 모음이 나오니까 이건 종성이 아님.
                    if (GetKor(eng, index + 1, 1, out len2) != '\0')
                    {
                        b.Append(Combine(ini, vow));
                        continue;
                    }

                    // 이게 종성이 맞음.
                    b.Append(Combine(ini, vow, und));
                    index += len;
                    continue;
                }
            }

            return b.ToString();
        }

        /// <summary>
        /// 한글이 안쳐져요 -> gksrmfdl dkscuwudy
        /// </summary>
        /// <param name="eng">변환할 문자열입니다.</param>
        /// <returns>변환된 결과값입니다</returns>
        /// <exception cref="ArgumentNullException">kor 가 Null 일때 발생합니다</exception>
        /// <exception cref="ArgumentException">kor 의 길이가 0 일때 발생합니다</exception>
        public static string Kor2Eng(string kor)
        {
            if (kor == null) throw new ArgumentNullException();
            if (kor.Length == 0) throw new ArgumentException();

            var sb = new StringBuilder(kor.Length * 2);
            int ini, vow, und;

            int i = 0;
            do
            {
                if (!Split(kor[i], out ini, out vow, out und))
                    sb.Append(kor[i]);
                else
                {
                    if (ini != -1) sb.Append(Table[Array.IndexOf<string>(Table, IniS[ini]) + 1]);
                    if (vow != -1) sb.Append(Table[Array.IndexOf<string>(Table, VolS[vow]) + 1]);
                    if (und > 0) sb.Append(Table[Array.IndexOf<string>(Table, UndS[und]) + 1]);
                }
            } while (++i < kor.Length);

            return sb.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (richTextBox2.Text == "" && richTextBox1.Text != "")
            {
                richTextBox2.Text = Eng2Kor(richTextBox1.Text);
            }
            else if (richTextBox1.Text == "" && richTextBox2.Text != "")
            {
                richTextBox1.Text = Kor2Eng(richTextBox2.Text);
            }
            else if (richTextBox1.Text == "" && richTextBox2.Text == "")
            {
                MessageBox.Show("입력");
            }

            else
            {
                richTextBox1.Clear();
                richTextBox2.Clear();
            }
        }

        private void TranslatorFrm_Load(object sender, EventArgs e)
        {
           

            if (File.Exists(DbPath))
            {
                string[] lines = File.ReadAllLines(DbPath);
                if (Convert.ToInt16(lines[2])>0)
                {
                    label5.Text = lines[2];
                    vScrollBar1.Value = Convert.ToInt16(lines[2]);
                    this.Opacity = (double)vScrollBar1.Value / vScrollBar1.Maximum; ;
                }
                else
                {
                    vScrollBar1.Value = 50;
                    this.Opacity = (double)vScrollBar1.Value / vScrollBar1.Maximum; ;
                }


            }
            else if (!File.Exists(DbPath))
            {
                vScrollBar1.Value = 50;
                this.Opacity = (double)vScrollBar1.Value / vScrollBar1.Maximum; ;
            }

            
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.Opacity = (double)vScrollBar1.Value / vScrollBar1.Maximum;
            //double db = (double)vScrollBar1.Value / vScrollBar1.Maximum;
            //label5.Text = vScrollBar1.Value.ToString();
            
        }

        private void vScrollBar1_MouseLeave(object sender, EventArgs e)
        {
            label5.Text = vScrollBar1.Value.ToString();

            if (File.Exists(DbPath))
            {
                var lines = File.ReadAllLines(DbPath).Count();
                if (lines==3)
                {
                    TxtlineChanger(label5.Text, DbPath, 3);
                }
                
            }

        }

        static void TxtlineChanger(string newText, string fileName, int line_to_edit)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit - 1] = newText;
            File.WriteAllLines(fileName, arrLine);
        }


    }
}
