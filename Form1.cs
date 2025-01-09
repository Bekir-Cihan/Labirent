using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LabirentOyunu
{
    public partial class Form1 : Form
    {
        // Sabitler
        const int BOYUT = 8;             // Labirent boyutu
        const int HUCREDEN_BOYUT = 50;   // Her hücrenin boyutu (piksel)
        const int DUVAR = 1;            // Duvar tanımı
        const int YOL = 0;              // Yol tanımı
        const int CIKIS = 2;            // Çıkış tanımı

        // Oyuncu konumu
        int oyuncuX = 1, oyuncuY = 1;
        int cikisX = BOYUT - 2, cikisY = BOYUT - 2;

        // Diğer değişkenler
        Button[,] buttons = new Button[BOYUT, BOYUT]; // Butonlar (hücreler)
        int adimSayisi = 0;                           // Adım sayacı
        List<string> izlenenYol = new List<string>(); // Yol kaydı

        public Form1()
        {
            InitializeComponent(); // Tasarım yüklenir
            LabirentOlustur();     // Labirent oluşturulur
            this.KeyPreview = true; // Klavye girişleri aktif edilir
            this.KeyDown += Form1_KeyDown; // Klavye event atanır
        }

        // Labirent oluşturma
        private void LabirentOlustur()
        {
            for (int i = 0; i < BOYUT; i++)
            {
                for (int j = 0; j < BOYUT; j++)
                {
                    // Her bir hücre için buton oluştur
                    Button btn = new Button();
                    btn.Width = HUCREDEN_BOYUT;
                    btn.Height = HUCREDEN_BOYUT;
                    btn.Left = j * HUCREDEN_BOYUT;
                    btn.Top = i * HUCREDEN_BOYUT;
                    btn.Tag = $"{i},{j}"; // Hücre koordinatlarını tut

                    // Kenarlara duvar koy
                    if (i == 0 || j == 0 || i == BOYUT - 1 || j == BOYUT - 1)
                    {
                        btn.BackColor = Color.Black; // Duvar rengi siyah
                    }
                    else
                    {
                        btn.BackColor = Color.White; // Yol alanı beyaz
                    }

                    // Çıkış noktası
                    if (i == cikisX && j == cikisY)
                    {
                        btn.BackColor = Color.Green; // Çıkış rengi yeşil
                        btn.Text = "E"; // Çıkış etiketi
                    }

                    // Butonu forma ekle
                    this.Controls.Add(btn);
                    buttons[i, j] = btn; // Dizide sakla
                }
            }

            // Oyuncunun başlangıç konumu
            buttons[oyuncuX, oyuncuY].BackColor = Color.Red; // Oyuncu rengi
        }

        // Oyuncu hareketleri
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int yeniX = oyuncuX, yeniY = oyuncuY;

            // Hareket yönü belirle
            switch (e.KeyCode)
            {
                case Keys.Up: yeniX--; break;   // Yukarı
                case Keys.Down: yeniX++; break; // Aşağı
                case Keys.Left: yeniY--; break; // Sol
                case Keys.Right: yeniY++; break; // Sağ
            }

            // Hareket kontrolü
            if (yeniX >= 0 && yeniY >= 0 && yeniX < BOYUT && yeniY < BOYUT &&
                buttons[yeniX, yeniY].BackColor != Color.Black)
            {
                // Önceki konumu temizle
                buttons[oyuncuX, oyuncuY].BackColor = Color.White;

                // Yeni konuma taşı
                oyuncuX = yeniX;
                oyuncuY = yeniY;

                // Oyuncuyu göster
                buttons[oyuncuX, oyuncuY].BackColor = Color.Red;
                adimSayisi++;
                izlenenYol.Add($"({oyuncuX}, {oyuncuY})");

                // Çıkış kontrolü
                if (oyuncuX == cikisX && oyuncuY == cikisY)
                {
                    OyunSonu();
                }
            }
        }

        // Oyun bitiş durumu
        private void OyunSonu()
        {
            MessageBox.Show($"Tebrikler! Oyunu bitirdiniz.\nAdım Sayısı: {adimSayisi}", "Oyun Bitti");
            AnalizGoster();
        }

        // Analiz raporu
        private void AnalizGoster()
        {
            string analiz = "İzlenen Yol:\n";
            foreach (var yol in izlenenYol)
            {
                analiz += yol + "\n";
            }
            MessageBox.Show(analiz, "Analiz");
        }
    }
}
