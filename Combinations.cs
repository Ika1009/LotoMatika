using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Loto_App
{
    class Combinations
    {
        static int _indeks_min(int[] niz, int duzina_niza)  //NALAZENJE NAJMANJEG CLANA NIZA
        {
            int min = 0;
            for (int i = 0; i < duzina_niza; i++)
            {
                if (niz[i] < niz[min])
                {
                    min = i;
                }
            }

            return min;
        }

        static int _indeks_max(int[] niz, int duzina_niza)  //NALAZENJE NAJVECEG CLANA NIZA
        {
            int max = 0;
            for (int i = 0; i < duzina_niza; i++)
            {
                if (niz[i] > niz[max])
                {
                    max = i;
                }
            }

            return max;
        }

        static int _indeks_dupli(int[] niz, int duzina_niza, int najveci_broj)  //NALAZENJE PRVOG DUPLIRANOG CLANA NIZA
        {
            int[] vidjeni_brojevi = new int[najveci_broj];
            for (int i = 0; i < najveci_broj; i++)
                vidjeni_brojevi[i] = 0;

            for (int i = 0; i < duzina_niza; i++)
            {
                if (vidjeni_brojevi[niz[i] - 1] > 0)
                {
                    return i;
                }
                vidjeni_brojevi[niz[i] - 1] = 1;
            }

            return -1;
        }

        static int _indeks_parni(int[] niz, int duzina_niza)  //NALAZENJE PRVOG PARNOG CLANA NIZA
        {
            for (int i = 0; i < duzina_niza; i++)
                if (niz[i] % 2 == 0)
                    return i;

            return -1;
        }

        static int _indeks_neparni(int[] niz, int duzina_niza)  //NALAZENJE PRVOG PARNOG CLANA NIZA
        {
            for (int i = 0; i < duzina_niza; i++)
                if (niz[i] % 2 != 0)
                    return i;

            return -1;
        }

        static int _indeks_mali(int[] niz, int duzina_niza, int granica_malih)  //NALAZENJE PRVOG PARNOG CLANA NIZA
        {
            for (int i = 0; i < duzina_niza; i++)
                if (niz[i] <= granica_malih)
                    return i;

            return -1;
        }
        static int _indeks_veliki(int[] niz, int duzina_niza, int granica_malih)  //NALAZENJE PRVOG PARNOG CLANA NIZA
        {
            for (int i = 0; i < duzina_niza; i++)
                if (niz[i] > granica_malih)
                    return i;

            return -1;
        }

        static int _indeks_zadnja_cifra(int[] niz, int duzina_niza, int[] zadnje_cifre)  //NALAZENJE CLANA NIZA SA ISTOM ZADNJOM CIFROM
        {
            int par = -1;
            int broj_parova = 0;
            for (int i = 0; i < 10; i++)
            {
                if (zadnje_cifre[i] == 2)
                    broj_parova++;
                else if (zadnje_cifre[i] >= 3)
                {
                    par = i;
                    break;
                }
                if (broj_parova >= 2)
                {
                    par = i;
                    break;
                }
            }
            if (par == -1)
                return -1;
            for (int i = 0; i < duzina_niza; i++)
                if (niz[i] % 10 == par)
                    return i;

            return -1;
        }

        static int _indeks_susedni(int[] niz, int duzina_niza)  //NALAZENJE UZASTOPNOG CLANA NIZA
        {
            int[] sortiran_niz = new int[duzina_niza];
            for (int i = 0; i < duzina_niza; i++)
                sortiran_niz[i] = niz[i];
            Array.Sort(sortiran_niz);
            int par = -1;
            int broj_parova = 0;
            for (int i = 1; i < duzina_niza; i++)
            {
                if (sortiran_niz[i] == (sortiran_niz[i - 1] + 1))
                    broj_parova++;
                if (broj_parova >= 2)
                {
                    par = sortiran_niz[i];
                    break;
                }
            }
            if (par == -1)
                return -1;
            for (int i = 0; i < duzina_niza; i++)
                if (niz[i] == par)
                    return i;

            return -1;
        }

        static int _indeks_skupovi_visak(int[] niz, int duzina_niza, int velicina_skupa)  //NALAZENJE VISAK CLANA NIZA SKUPA
        {
            int[] skupovi = new int[5];
            for (int i = 0; i < 5; i++)
                skupovi[i] = 0;
            int broj_praznih_skupova = 0;
            int skup_sa_viskom = -1;
            for (int i = 0; i < duzina_niza; i++)
                skupovi[(niz[i] - 1) / velicina_skupa]++;
            for (int i = 0; i < 5; i++)
                if (skupovi[i] == 0)
                    broj_praznih_skupova++;
            if (broj_praznih_skupova <= 1)
                return -1;
            else if (broj_praznih_skupova > 1)
                for (int i = 0; i < 5; i++)
                    if (skupovi[i] > 1)
                        skup_sa_viskom = i;
            for (int i = 0; i < duzina_niza; i++)
                if ((niz[i] > (skup_sa_viskom * velicina_skupa)) && (niz[i] <= ((skup_sa_viskom + 1) * velicina_skupa)))
                    return i;

            return -1;
        }

        static bool _poseduje_element(int[] niz, int duzina_niza, int element)  //DA LI NIZ POSEDUJE DATI ELEMENT
        {
            for (int i = 0; i < duzina_niza; i++)
                if (niz[i] == element)
                    return true;

            return false;
        }

        static int _broj_parnih(int[] niz, int duzina_niza)  //KOLICINA PARNIH BROJEVA
        {
            int broj_parnih = 0;
            for (int i = 0; i < duzina_niza; i++)
                if (niz[i] % 2 == 0)
                    broj_parnih++;

            return broj_parnih;
        }

        static int _broj_malih(int[] niz, int duzina_niza, int granica_malih)  //KOLICINA PARNIH BROJEVA
        {
            int broj_malih = 0;
            for (int i = 0; i < duzina_niza; i++)

                if (niz[i] <= granica_malih)
                    broj_malih++;

            return broj_malih;
        }

        static int _generisi_kombinacije(int[][] brojevi, int[] trenutna_kombinacija, int red, int trenutni_broj, int clan_kombinacije, int broj_loptica, int duzina_kombinacije)    //GENERACIJA SVIH KOMBINACIJA
        {
            if (trenutni_broj > broj_loptica)
                return red;
            if (clan_kombinacije == duzina_kombinacije)
            {
                for (int i = trenutni_broj; i <= broj_loptica; i++)
                {
                    trenutna_kombinacija[clan_kombinacije - 1] = i;
                    for (int j = 0; j < duzina_kombinacije; j++)
                        brojevi[red][j] = trenutna_kombinacija[j];
                    red++;
                }
                return red;
            }
            for (int i = trenutni_broj; i <= broj_loptica; i++)
            {
                trenutna_kombinacija[clan_kombinacije - 1] = i;
                red = _generisi_kombinacije(brojevi, trenutna_kombinacija, red, i + 1, clan_kombinacije + 1, broj_loptica, duzina_kombinacije);
            }
            return red;
        }

        static void Mainara(string[] args)
        {
            string biranje_moda = Console.ReadLine();  //BIRANJE MODA
            if (biranje_moda == "neke")
            {
                int broj_loptica = int.Parse(Console.ReadLine());   //UNOS
                int duzina_kombinacije = int.Parse(Console.ReadLine());
                int broj_kombinacija = int.Parse(Console.ReadLine());

                int granica_malih = 0;  //STVARANJE GRANICE MALI/VELIKI
                if (duzina_kombinacije == 6)
                {
                    if (broj_loptica == 39)
                        granica_malih = 19;
                    else if (broj_loptica == 44)
                        granica_malih = 22;
                    else if (broj_loptica == 45)
                        granica_malih = 22;
                }
                else if (duzina_kombinacije == 7)
                {
                    if (broj_loptica == 35)
                        granica_malih = 17;
                    else if (broj_loptica == 37)
                        granica_malih = 18;
                    else if (broj_loptica == 39)
                        granica_malih = 19;
                }

                bool[] upotrebljeni_brojevi = new bool[broj_loptica];    //LISTA UPOTREBLJENIH BROJEVA
                for (int i = 0; i < broj_loptica; i++)
                    upotrebljeni_brojevi[i] = false;

                int ukupan_broj_parnih = 0; //STVARANJE BROJEVA
                int ukupan_broj_neparnih = 0;
                int ukupan_broj_malih = 0;
                int ukupan_broj_velikih = 0;
                int broj_brojeva = broj_kombinacija * duzina_kombinacije;
                int[][] brojevi = new int[15400000][];
                for (int i = 0; i < (broj_brojeva / broj_loptica * broj_loptica); i++)
                {
                    if ((i % broj_loptica + 1) % 2 == 0)
                        ukupan_broj_parnih++;
                    else if ((i % broj_loptica + 1) % 2 != 0)
                        ukupan_broj_neparnih++;
                    if ((i % broj_loptica + 1) <= granica_malih)
                        ukupan_broj_malih++;
                    else if ((i % broj_loptica + 1) > granica_malih)
                        ukupan_broj_velikih++;
                    if ((i % broj_loptica) % duzina_kombinacije == 0)
                        brojevi[i / duzina_kombinacije] = new int[7];
                    brojevi[i / duzina_kombinacije][i % duzina_kombinacije] = i % broj_loptica + 1;
                }
                int trenutni_broj = 1;
                bool broj_nadjen = false;
                for (int i = (broj_brojeva / broj_loptica * broj_loptica); i < broj_brojeva; i++)
                {
                    broj_nadjen = false;
                    while (broj_nadjen == false)
                    {
                        if (upotrebljeni_brojevi[trenutni_broj - 1] == true)
                            trenutni_broj++;
                        else if ((trenutni_broj % 2 == 0) && (ukupan_broj_parnih * 2 == broj_brojeva))
                            trenutni_broj++;
                        else if ((trenutni_broj % 2 != 0) && (ukupan_broj_neparnih * 2 == broj_brojeva))
                            trenutni_broj++;
                        else if (trenutni_broj <= granica_malih && (ukupan_broj_malih * 2 == broj_brojeva))
                            trenutni_broj++;
                        else if (trenutni_broj > granica_malih && (ukupan_broj_velikih * 2 == broj_brojeva))
                            trenutni_broj++;
                        else
                        {
                            broj_nadjen = true;
                            upotrebljeni_brojevi[(trenutni_broj % broj_loptica) - 1] = true;
                        }
                        if (trenutni_broj > broj_loptica)
                            trenutni_broj = 1;
                    }
                    if (trenutni_broj % 2 == 0)
                        ukupan_broj_parnih++;
                    else if (trenutni_broj % 2 != 0)
                        ukupan_broj_neparnih++;
                    if (trenutni_broj <= granica_malih)
                        ukupan_broj_malih++;
                    else if (trenutni_broj > granica_malih)
                        ukupan_broj_velikih++;
                    if (i % duzina_kombinacije == 0)
                        brojevi[i / duzina_kombinacije] = new int[7];
                    brojevi[i / duzina_kombinacije][i % duzina_kombinacije] = trenutni_broj;
                }

                /*Random random = new Random();  //MESANJE BROJEVA
                for (int i = broj_brojeva - 1; i > 0; i--)
                {
                    int j = random.Next(0, i + 1);

                    int red_i = i / duzina_kombinacije;
                    int kolona_i = i % duzina_kombinacije;
                    int red_j = j / duzina_kombinacije;
                    int kolona_j = j % duzina_kombinacije;

                    int temp = brojevi[red_i][kolona_i];
                    brojevi[red_i][kolona_i] = brojevi[red_j][kolona_j];
                    brojevi[red_j][kolona_j] = temp;
                }

                int[][] tabela1 = new int[0][];    //TABELE PARNI/NEPARNI, MALI/VELIKI
                int[] tabela_parni = new int[0];
                int[] tabela_mali = new int[0];
                int broj_slucajeva = 0;
                if (duzina_kombinacije == 6)
                {
                    tabela1 = new int[][]
                    {
                        new int[] { 2, 1, 1, 1, 1, 1, 1, 1, 1 },
                        new int[] { 4, 2, 2, 2, 2, 2, 2, 2, 2 },
                        new int[] { 6, 3, 3, 3, 3, 3, 3, 3, 3 },
                        new int[] { 6, 5, 5, 5, 5, 4, 3, 4, 3 },
                        new int[] { 8, 6, 6, 6, 6, 5, 4, 5, 4 },
                        new int[] { 10, 7, 7, 7, 7, 6, 5, 6, 5 },
                        new int[] { 12, 9, 8, 9, 8, 6, 6, 6, 6 },
                        new int[] { 12, 10, 10, 10, 10, 7, 7, 7, 7 },
                        new int[] { 14, 11, 10, 11, 10, 9, 8, 9, 8 },
                        new int[] { 16, 12, 12, 12, 12, 9, 9, 9, 9 }
                    };
                    tabela_parni = new int[] { 3, 3, 3, 2, 2, 2, 4, 4, 4 };
                    tabela_mali = new int[] { 3, 2, 4, 3, 2, 4, 3, 2, 4 };
                    broj_slucajeva = 9;
                }
                else if (duzina_kombinacije == 7)
                {
                    tabela1 = new int[][]
                    {
                    new int[] { 3, 2, 3, 2 },
                    new int[] { 5, 5, 5, 5 },
                    new int[] { 8, 7, 8, 7 },
                    new int[] { 10, 10, 10, 10 },
                    new int[] { 13, 12, 13, 12 },
                    new int[] { 15, 15, 15, 15 },
                    new int[] { 18, 17, 18, 17 },
                    new int[] { 20, 20, 20, 20 },
                    new int[] { 23, 22, 23, 22 },
                    new int[] { 25, 25, 25, 25 }
                    };
                    tabela_parni = new int[] { 3, 3, 4, 4 };
                    tabela_mali = new int[] { 3, 4, 4, 3 };
                    broj_slucajeva = 4;
                }
                int[] broj_parnih_brojeva_kombinacija = new int[15400000];
                int broj_predjenih_kombinacija = 0;
                int inkrement_kombinacija;
                while (broj_predjenih_kombinacija < broj_kombinacija)
                {
                    if ((broj_kombinacija - broj_predjenih_kombinacija) > 100)
                        inkrement_kombinacija = 100;
                    else
                        inkrement_kombinacija = broj_kombinacija - broj_predjenih_kombinacija;

                    for (int i = 0; i < broj_slucajeva; i++)
                    {
                        for (int j = 0; j < tabela1[(inkrement_kombinacija / 10) - 1][i]; j++)
                        {
                            broj_parnih_brojeva_kombinacija[broj_predjenih_kombinacija] = tabela_parni[i];
                            broj_predjenih_kombinacija++;
                        }
                    }
                }
                int[] broj_malih_brojeva_kombinacija = new int[15400000];
                broj_predjenih_kombinacija = 0;
                while (broj_predjenih_kombinacija < broj_kombinacija)
                {
                    if ((broj_kombinacija - broj_predjenih_kombinacija) > 100)
                        inkrement_kombinacija = 100;
                    else
                        inkrement_kombinacija = broj_kombinacija - broj_predjenih_kombinacija;

                    for (int i = 0; i < broj_slucajeva; i++)
                    {
                        for (int j = 0; j < tabela1[(inkrement_kombinacija / 10) - 1][i]; j++)
                        {
                            broj_malih_brojeva_kombinacija[broj_predjenih_kombinacija] = tabela_mali[i];
                            broj_predjenih_kombinacija++;
                        }
                    }
                }

                bool svi_parni_su_dobri = false;    //RESAVANJE PARNIH
                while (!svi_parni_su_dobri)
                {
                    svi_parni_su_dobri = true;
                    for (int i = 0; i < broj_kombinacija; i++)
                    {
                        if (broj_parnih_brojeva_kombinacija[i] < _broj_parnih(brojevi[i], duzina_kombinacije))
                        {
                            svi_parni_su_dobri = false;
                            for (int j = 0; (j < broj_kombinacija) && (broj_parnih_brojeva_kombinacija[i] < _broj_parnih(brojevi[i], duzina_kombinacije)); j++)
                                if (broj_parnih_brojeva_kombinacija[j] > _broj_parnih(brojevi[j], duzina_kombinacije))
                                {
                                    int temp = brojevi[i][_indeks_parni(brojevi[i], duzina_kombinacije)];
                                    brojevi[i][_indeks_parni(brojevi[i], duzina_kombinacije)] = brojevi[j][_indeks_neparni(brojevi[j], duzina_kombinacije)];
                                    brojevi[j][_indeks_neparni(brojevi[j], duzina_kombinacije)] = temp;
                                }
                        }
                    }
                }

                int brojac = 0;
                bool svi_mali_su_dobri = false; //RESAVANJE MALIH
                while (!svi_mali_su_dobri)
                {
                    brojac++;
                    svi_mali_su_dobri = true;
                    for (int i = 0; i < broj_kombinacija; i++)
                    {
                        if (broj_malih_brojeva_kombinacija[i] < _broj_malih(brojevi[i], duzina_kombinacije, granica_malih))
                        {
                            svi_mali_su_dobri = false;
                            for (int j = 0; (j < broj_kombinacija) && (broj_malih_brojeva_kombinacija[i] < _broj_malih(brojevi[i], duzina_kombinacije, granica_malih)); j++)
                                if (broj_malih_brojeva_kombinacija[j] > _broj_malih(brojevi[j], duzina_kombinacije, granica_malih)
                                && ((brojevi[i][_indeks_mali(brojevi[i], duzina_kombinacije, granica_malih)] % 2) == (brojevi[j][_indeks_veliki(brojevi[j], duzina_kombinacije, granica_malih)] % 2)))
                                {
                                    int temp = brojevi[i][_indeks_mali(brojevi[i], duzina_kombinacije, granica_malih)];
                                    brojevi[i][_indeks_mali(brojevi[i], duzina_kombinacije, granica_malih)] = brojevi[j][_indeks_veliki(brojevi[j], duzina_kombinacije, granica_malih)];
                                    brojevi[j][_indeks_veliki(brojevi[j], duzina_kombinacije, granica_malih)] = temp;
                                }
                        }
                    }
                    if (brojac == 1000)
                        break;
                }*/

                /*bool svi_duplikati_su_dobri = false;    //RESAVANJE DUPLIKATA
                while (!svi_duplikati_su_dobri)
                {
                    svi_duplikati_su_dobri = true;
                    for (int i = 0; i < broj_kombinacija; i++)
                    {
                        if (_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica) != -1)  //ZA VECE
                        {
                            svi_duplikati_su_dobri = false;
                            for (int j = 0; (j < broj_kombinacija) && (_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica) != -1); j++)
                            {
                                if (!_poseduje_element(brojevi[j], duzina_kombinacije, _indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)))
                                {
                                    int temp = brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)];
                                    brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)] = brojevi[j][0];
                                    brojevi[j][0] = temp;
                                }
                            }
                        }
                    }
                }

                int[] sume_kombinacija = new int[15400000]; //STVARANJE SUMA
                int suma;
                for (int i = 0; i < broj_kombinacija; i++)
                {
                    suma = 0;
                    for (int j = 0; j < duzina_kombinacije; j++)
                        suma += brojevi[i][j];
                    sume_kombinacija[i] = suma;
                }
                int suma_min = int.MinValue, suma_max = int.MaxValue;
                if (duzina_kombinacije == 6)
                {
                    if (broj_loptica == 39)
                    {
                        suma_min = 79;
                        suma_max = 159;
                    }
                    else if (broj_loptica == 44)
                    {
                        suma_min = 100;
                        suma_max = 179;
                    }
                    else if (broj_loptica == 45)
                    {
                        suma_min = 100;
                        suma_max = 179;
                    }
                }
                else if (duzina_kombinacije == 7)
                {
                    if (broj_loptica == 35)
                    {
                        suma_min = 79;
                        suma_max = 169;
                    }
                    else if (broj_loptica == 37)
                    {
                        suma_min = 79;
                        suma_max = 179;
                    }
                    else if (broj_loptica == 39)
                    {
                        suma_min = 79;
                        suma_max = 179;
                    }
                }

                int nova_suma1; //RESAVANJE SUMA
                int nova_suma2;
                int nasumicni_element;
                bool sve_sume_su_dobre = false;
                while (!sve_sume_su_dobre)
                {
                    sve_sume_su_dobre = true;
                    for (int i = 0; i < broj_kombinacija; i++)
                    {
                        if (sume_kombinacija[i] < suma_min)  //ZA MANJE
                        {
                            sve_sume_su_dobre = false;
                            for (int j = 0; (j < broj_kombinacija) && (sume_kombinacija[i] < suma_min); j++)
                                for (int k = 0; (k < duzina_kombinacije) && (sume_kombinacija[i] < suma_min); k++)
                                {
                                    nasumicni_element = random.Next(0, duzina_kombinacije);
                                    nova_suma1 = sume_kombinacija[i] - brojevi[i][nasumicni_element] + brojevi[j][k];
                                    nova_suma2 = sume_kombinacija[j] + brojevi[i][nasumicni_element] - brojevi[j][k];
                                    if ((nova_suma1 <= suma_max)
                                    && (nova_suma2 >= suma_min) && (nova_suma2 <= suma_max)
                                    && (nova_suma1 > sume_kombinacija[i])
                                    && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][nasumicni_element])
                                    && !_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k]))
                                    {
                                        int temp = brojevi[i][nasumicni_element];
                                        brojevi[i][nasumicni_element] = brojevi[j][k];
                                        brojevi[j][k] = temp;
                                        sume_kombinacija[i] = nova_suma1;
                                        sume_kombinacija[j] = nova_suma2;
                                    }
                                }
                        }
                        else if (sume_kombinacija[i] > suma_max)  //ZA VECE
                        {
                            sve_sume_su_dobre = false;
                            for (int j = 0; (j < broj_kombinacija) && (sume_kombinacija[i] > suma_max); j++)
                                for (int k = 0; (k < duzina_kombinacije) && (sume_kombinacija[i] > suma_max); k++)
                                {
                                    nasumicni_element = random.Next(0, duzina_kombinacije);
                                    nova_suma1 = sume_kombinacija[i] - brojevi[i][nasumicni_element] + brojevi[j][k];
                                    nova_suma2 = sume_kombinacija[j] + brojevi[i][nasumicni_element] - brojevi[j][k];
                                    if ((nova_suma1 >= suma_min)
                                    && (nova_suma2 >= suma_min) && (nova_suma2 <= suma_max)
                                    && (nova_suma1 < sume_kombinacija[i])
                                    && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][nasumicni_element])
                                    && !_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k]))
                                    {
                                        int temp = brojevi[i][nasumicni_element];
                                        brojevi[i][nasumicni_element] = brojevi[j][k];
                                        brojevi[j][k] = temp;
                                        sume_kombinacija[i] = nova_suma1;
                                        sume_kombinacija[j] = nova_suma2;
                                    }
                                }
                        }
                    }
                }

                int[] razlike_kombinacija = new int[15400000]; //STVARANJE RAZLIKA
                int razlika;
                for (int i = 0; i < broj_kombinacija; i++)
                    razlike_kombinacija[i] = brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] - brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)];
                int razlika_min = int.MinValue, razlika_max = int.MaxValue;
                if (duzina_kombinacije == 6)
                {
                    if (broj_loptica == 39)
                    {
                        razlika_min = 23;
                        razlika_max = 37;
                    }
                    else if (broj_loptica == 44)
                    {
                        razlika_min = 24;
                        razlika_max = 42;
                    }
                    else if (broj_loptica == 45)
                    {
                        razlika_min = 25;
                        razlika_max = 43;
                    }
                }
                else if (duzina_kombinacije == 7)
                {
                    if (broj_loptica == 35)
                    {
                        razlika_min = 21;
                        razlika_max = 33;
                    }
                    else if (broj_loptica == 37)
                    {
                        razlika_min = 21;
                        razlika_max = 35;
                    }
                    else if (broj_loptica == 39)
                    {
                        razlika_min = 23;
                        razlika_max = 37;
                    }
                }

                int nova_razlika1; //RESAVANJE RAZLIKA
                int nova_razlika2;
                int[] privremena_kombinacija2 = new int[7];
                int izabran_broj;
                bool sve_razlike_su_dobre = false;
                while (!sve_razlike_su_dobre)
                {
                    sve_razlike_su_dobre = true;
                    for (int i = 0; i < broj_kombinacija; i++)
                    {
                        if (razlike_kombinacija[i] < razlika_min)  //ZA MANJE
                        {
                            sve_razlike_su_dobre = false;
                            for (int j = 0; (j < broj_kombinacija) && (razlike_kombinacija[i] < razlika_min); j++)
                                for (int k = 0; (k < duzina_kombinacije) && (razlike_kombinacija[i] < razlika_min); k++)
                                {
                                    izabran_broj = random.Next(1, 3);
                                    if (izabran_broj == 1)  //SMANJUJE NAJMANJI
                                    {
                                        nova_razlika1 = brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] - brojevi[j][k];
                                        for (int l = 0; l < duzina_kombinacije; l++)
                                            privremena_kombinacija2[l] = brojevi[j][l];
                                        privremena_kombinacija2[k] = brojevi[j][_indeks_min(brojevi[j], duzina_kombinacije)];
                                        nova_razlika2 = privremena_kombinacija2[_indeks_max(privremena_kombinacija2, duzina_kombinacije)] - privremena_kombinacija2[_indeks_min(privremena_kombinacija2, duzina_kombinacije)];

                                        nova_suma1 = sume_kombinacija[i] - brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] + brojevi[j][k];
                                        nova_suma2 = sume_kombinacija[j] + brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] - brojevi[j][k];
                                        if ((nova_razlika1 <= razlika_max)
                                        && (nova_razlika2 >= razlika_min) && (nova_razlika2 <= razlika_max)
                                        && (nova_razlika1 > razlike_kombinacija[i])
                                        && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)])
                                        && !_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k])
                                        && (nova_suma1 >= suma_min) && (nova_suma1 <= suma_max)
                                        && (nova_suma2 >= suma_min) && (nova_suma2 <= suma_max))
                                        {
                                            sume_kombinacija[i] = nova_suma1;
                                            sume_kombinacija[j] = nova_suma2;

                                            int temp = brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)];
                                            brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] = brojevi[j][k];
                                            brojevi[j][k] = temp;
                                            razlike_kombinacija[i] = nova_razlika1;
                                            razlike_kombinacija[j] = nova_razlika2;
                                        }
                                    }
                                    else if (izabran_broj == 2) //POVECAVA NAJVECI
                                    {
                                        nova_razlika1 = brojevi[j][k] - brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)];
                                        for (int l = 0; l < duzina_kombinacije; l++)
                                            privremena_kombinacija2[l] = brojevi[j][l];
                                        privremena_kombinacija2[k] = brojevi[j][_indeks_max(brojevi[j], duzina_kombinacije)];
                                        nova_razlika2 = privremena_kombinacija2[_indeks_max(privremena_kombinacija2, duzina_kombinacije)] - privremena_kombinacija2[_indeks_min(privremena_kombinacija2, duzina_kombinacije)];

                                        nova_suma1 = sume_kombinacija[i] - brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] + brojevi[j][k];
                                        nova_suma2 = sume_kombinacija[j] + brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] - brojevi[j][k];
                                        if ((nova_razlika1 <= razlika_max)
                                        && (nova_razlika2 >= razlika_min) && (nova_razlika2 <= razlika_max)
                                        && (nova_razlika1 > razlike_kombinacija[i])
                                        && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)])
                                        && !_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k])
                                        && (nova_suma1 >= suma_min) && (nova_suma1 <= suma_max)
                                        && (nova_suma2 >= suma_min) && (nova_suma2 <= suma_max))
                                        {
                                            sume_kombinacija[i] = nova_suma1;
                                            sume_kombinacija[j] = nova_suma2;

                                            int temp = brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)];
                                            brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] = brojevi[j][k];
                                            brojevi[j][k] = temp;
                                            razlike_kombinacija[i] = nova_razlika1;
                                            razlike_kombinacija[j] = nova_razlika2;
                                        }
                                    }
                                }
                        }
                        else if (razlike_kombinacija[i] > razlika_max)  //ZA VECE
                        {
                            sve_razlike_su_dobre = false;
                            for (int j = 0; (j < broj_kombinacija) && (razlike_kombinacija[i] > razlika_max); j++)
                                for (int k = 0; (k < duzina_kombinacije) && (razlike_kombinacija[i] > razlika_max); k++)
                                {
                                    izabran_broj = random.Next(1, 3);
                                    if (izabran_broj == 1)  //POVECAVA NAJMANJI
                                    {
                                        nova_razlika1 = brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] - brojevi[j][k];
                                        for (int l = 0; l < duzina_kombinacije; l++)
                                            privremena_kombinacija2[l] = brojevi[j][l];
                                        privremena_kombinacija2[k] = brojevi[j][_indeks_min(brojevi[j], duzina_kombinacije)];
                                        nova_razlika2 = privremena_kombinacija2[_indeks_max(privremena_kombinacija2, duzina_kombinacije)] - privremena_kombinacija2[_indeks_min(privremena_kombinacija2, duzina_kombinacije)];

                                        nova_suma1 = sume_kombinacija[i] - brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] + brojevi[j][k];
                                        nova_suma2 = sume_kombinacija[j] + brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] - brojevi[j][k];
                                        if ((nova_razlika1 >= razlika_min)
                                        && (nova_razlika2 >= razlika_min) && (nova_razlika2 <= razlika_max)
                                        && (nova_razlika1 < razlike_kombinacija[i])
                                        && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)])
                                        && !_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k])
                                        && (nova_suma1 >= suma_min) && (nova_suma1 <= suma_max)
                                        && (nova_suma2 >= suma_min) && (nova_suma2 <= suma_max))
                                        {
                                            sume_kombinacija[i] = nova_suma1;
                                            sume_kombinacija[j] = nova_suma2;

                                            int temp = brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)];
                                            brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] = brojevi[j][k];
                                            brojevi[j][k] = temp;
                                            razlike_kombinacija[i] = nova_razlika1;
                                            razlike_kombinacija[j] = nova_razlika2;
                                        }
                                    }
                                    else if (izabran_broj == 2) //SMANJUJE NAJVECI
                                    {
                                        nova_razlika1 = brojevi[j][k] - brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)];
                                        for (int l = 0; l < duzina_kombinacije; l++)
                                            privremena_kombinacija2[l] = brojevi[j][l];
                                        privremena_kombinacija2[k] = brojevi[j][_indeks_max(brojevi[j], duzina_kombinacije)];
                                        nova_razlika2 = privremena_kombinacija2[_indeks_max(privremena_kombinacija2, duzina_kombinacije)] - privremena_kombinacija2[_indeks_min(privremena_kombinacija2, duzina_kombinacije)];

                                        nova_suma1 = sume_kombinacija[i] - brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] + brojevi[j][k];
                                        nova_suma2 = sume_kombinacija[j] + brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] - brojevi[j][k];
                                        if ((nova_razlika1 >= razlika_min)
                                        && (nova_razlika2 >= razlika_min) && (nova_razlika2 <= razlika_max)
                                        && (nova_razlika1 < razlike_kombinacija[i])
                                        && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)])
                                        && !_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k])
                                        && (nova_suma1 >= suma_min) && (nova_suma1 <= suma_max)
                                        && (nova_suma2 >= suma_min) && (nova_suma2 <= suma_max))
                                        {
                                            sume_kombinacija[i] = nova_suma1;
                                            sume_kombinacija[j] = nova_suma2;

                                            int temp = brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)];
                                            brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] = brojevi[j][k];
                                            brojevi[j][k] = temp;
                                            razlike_kombinacija[i] = nova_razlika1;
                                            razlike_kombinacija[j] = nova_razlika2;
                                        }
                                    }
                                }
                        }
                    }
                }*/

                for (int i = 0; i < broj_kombinacija; i++)  //ISPIS
                {
                    Console.Write((i + 1) + ":\t");    //REDNI BROJEVI

                    for (int j = 0; j < duzina_kombinacije; j++)    //KOMBINACIJE
                    {
                        if (brojevi[i][j] % 2 == 0)
                            Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(brojevi[i][j] + "\t");
                        Console.ResetColor();
                    }
                    Console.Write("\n");

                    /*if (_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica) == -1)   //DA LI IMA PONAVLJANJA BROJEVA
                        Console.Write("\t-1\t");
                    else
                        Console.Write("\t" + brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)] + "\t");

                    if ((sume_kombinacija[i] > suma_max) || (sume_kombinacija[i] < suma_min))   //DA LI JE SUMA U OPSEGU
                        Console.Write("-1\t");
                    else
                        Console.Write(sume_kombinacija[i] + "\t");

                    if ((razlike_kombinacija[i] > razlika_max) || (razlike_kombinacija[i] < razlika_min))   //RAZLIKA NAJMANJEG I NAJVECEG
                        Console.Write("-1\n");
                    else
                        Console.Write(razlike_kombinacija[i] + "\n");*/

                    /*Console.Write(broj_parnih_brojeva_kombinacija[i] + "\t");   //BROJ PARNIH BROJEVA U KOMBINACIJAMA

                    Console.Write(broj_malih_brojeva_kombinacija[i] + "\n");   //BROJ MALIH BROJEVA U KOMBINACIJAMA*/
                }
            }
            else if (biranje_moda == "sve")
            {
                int broj_loptica = int.Parse(Console.ReadLine());   //UNOS
                int duzina_kombinacije = int.Parse(Console.ReadLine());

                int rezervisano_mesto = 15400000;   //REZERVISANJE MESTA
                if (duzina_kombinacije == 6)
                {
                    if (broj_loptica == 39)
                        rezervisano_mesto = 3300000;
                    else if (broj_loptica == 44)
                        rezervisano_mesto = 7100000;
                    else if (broj_loptica == 45)
                        rezervisano_mesto = 8200000;
                }
                else if (duzina_kombinacije == 7)
                {
                    if (broj_loptica == 35)
                        rezervisano_mesto = 7000000;
                    else if (broj_loptica == 37)
                        rezervisano_mesto = 10300000;
                    else if (broj_loptica == 39)
                        rezervisano_mesto = 15400000;
                }

                int[][] brojevi = new int[15400000][];  //STVARANJE SVIH KOMBINACIJA
                int[] trenutna_kombinacija = new int[7];
                for (int i = 0; i < rezervisano_mesto; i++)
                    brojevi[i] = new int[7];
                int red = 0;
                red = _generisi_kombinacije(brojevi, trenutna_kombinacija, red, 1, 1, broj_loptica, duzina_kombinacije);

                int[] sume_kombinacija = new int[15400000]; //STVARANJE SUMA
                int suma;
                for (int i = 0; i < red; i++)
                {
                    suma = 0;
                    for (int j = 0; j < duzina_kombinacije; j++)
                        suma += brojevi[i][j];
                    sume_kombinacija[i] = suma;
                }
                int suma_min = int.MinValue, suma_max = int.MaxValue;
                if (duzina_kombinacije == 6)
                {
                    if (broj_loptica == 39)
                    {
                        suma_min = 79;
                        suma_max = 159;
                    }
                    else if (broj_loptica == 44)
                    {
                        suma_min = 100;
                        suma_max = 179;
                    }
                    else if (broj_loptica == 45)
                    {
                        suma_min = 100;
                        suma_max = 179;
                    }
                }
                else if (duzina_kombinacije == 7)
                {
                    if (broj_loptica == 35)
                    {
                        suma_min = 79;
                        suma_max = 169;
                    }
                    else if (broj_loptica == 37)
                    {
                        suma_min = 79;
                        suma_max = 179;
                    }
                    else if (broj_loptica == 39)
                    {
                        suma_min = 79;
                        suma_max = 179;
                    }
                }

                int[] razlike_kombinacija = new int[15400000]; //STVARANJE RAZLIKA
                int razlika;
                for (int i = 0; i < red; i++)
                    razlike_kombinacija[i] = brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] - brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)];
                int razlika_min = int.MinValue, razlika_max = int.MaxValue;
                if (duzina_kombinacije == 6)
                {
                    if (broj_loptica == 39)
                    {
                        razlika_min = 23;
                        razlika_max = 37;
                    }
                    else if (broj_loptica == 44)
                    {
                        razlika_min = 24;
                        razlika_max = 42;
                    }
                    else if (broj_loptica == 45)
                    {
                        razlika_min = 25;
                        razlika_max = 43;
                    }
                }
                else if (duzina_kombinacije == 7)
                {
                    if (broj_loptica == 35)
                    {
                        razlika_min = 21;
                        razlika_max = 33;
                    }
                    else if (broj_loptica == 37)
                    {
                        razlika_min = 21;
                        razlika_max = 35;
                    }
                    else if (broj_loptica == 39)
                    {
                        razlika_min = 23;
                        razlika_max = 37;
                    }
                }

                int[] broj_parnih_brojeva_kombinacija = new int[15400000];  //STVARANJE PARNIH
                for (int i = 0; i < red; i++)
                    broj_parnih_brojeva_kombinacija[i] = _broj_parnih(brojevi[i], duzina_kombinacije);
                int parni_min = 0;
                int parni_max = 0;
                if (duzina_kombinacije == 6)
                {
                    parni_min = 2;
                    parni_max = 4;
                }
                else if (duzina_kombinacije == 7)
                {
                    parni_min = 3;
                    parni_max = 4;
                }

                int[] broj_malih_brojeva_kombinacija = new int[15400000];  //STVARANJE MALIH
                int mali_min = 0;
                int mali_max = 0;
                int granica_malih = 0;
                if (duzina_kombinacije == 6)
                {
                    mali_min = 2;
                    mali_max = 4;

                    if (broj_loptica == 39)
                        granica_malih = 19;
                    else if (broj_loptica == 44)
                        granica_malih = 22;
                    else if (broj_loptica == 45)
                        granica_malih = 22;
                }
                else if (duzina_kombinacije == 7)
                {
                    mali_min = 3;
                    mali_max = 4;

                    if (broj_loptica == 35)
                        granica_malih = 17;
                    else if (broj_loptica == 37)
                        granica_malih = 18;
                    else if (broj_loptica == 39)
                        granica_malih = 19;
                }
                for (int i = 0; i < red; i++)
                    broj_malih_brojeva_kombinacija[i] = _broj_malih(brojevi[i], duzina_kombinacije, granica_malih);

                int[][] zadnje_cifre = new int[15400000][];   //STVARANJE ZADNJIH CIFARA
                for (int i = 0; i < red; i++)
                {
                    zadnje_cifre[i] = new int[10];
                    for (int j = 0; j < duzina_kombinacije; j++)
                        zadnje_cifre[i][brojevi[i][j] % 10]++;
                }

                int velicina_skupa = 0;
                if (duzina_kombinacije == 6)
                {
                    if (broj_loptica == 39)
                        velicina_skupa = 8;
                    else if (broj_loptica == 44)
                        velicina_skupa = 9;
                    else if (broj_loptica == 45)
                        velicina_skupa = 9;
                }
                else if (duzina_kombinacije == 7)
                {
                    if (broj_loptica == 35)
                        velicina_skupa = 7;
                    else if (broj_loptica == 37)
                        velicina_skupa = 8;
                    else if (broj_loptica == 39)
                        velicina_skupa = 8;
                }

                int red2 = 0;
                for (int i = 0; i < red; i++)   //IZBACIVANJE NEPOTREBNIH KOMBINACIJA
                    if ((sume_kombinacija[i] <= suma_max) && (sume_kombinacija[i] >= suma_min)
                    && (razlike_kombinacija[i] <= razlika_max) && (razlike_kombinacija[i] >= razlika_min)
                    && (broj_parnih_brojeva_kombinacija[i] <= parni_max) && (broj_parnih_brojeva_kombinacija[i] >= parni_min)
                    && (broj_malih_brojeva_kombinacija[i] <= mali_max) && (broj_malih_brojeva_kombinacija[i] >= mali_min)
                    && (_indeks_zadnja_cifra(brojevi[i], duzina_kombinacije, zadnje_cifre[i]) == -1)
                    && (_indeks_susedni(brojevi[i], duzina_kombinacije) == -1)
                    && (_indeks_skupovi_visak(brojevi[i], duzina_kombinacije, velicina_skupa) == -1))
                    {
                        for (int j = 0; j < duzina_kombinacije; j++)
                            brojevi[red2][j] = brojevi[i][j];
                        red2++;
                    }

                for (int i = 0; i < red2; i++)  //ISPIS
                {
                    Console.Write((i + 1) + ":\t");    //REDNI BROJEVI

                    for (int j = 0; j < duzina_kombinacije; j++)    //KOMBINACIJE
                        Console.Write(brojevi[i][j] + "\t");
                    Console.Write("\n");

                    /*if (_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica) == -1)   //DA LI IMA PONAVLJANJA BROJEVA
                        Console.Write("-1\t");
                    else
                        Console.Write("brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)] + "\t");

                    if ((sume_kombinacija[i] > suma_max) || (sume_kombinacija[i] < suma_min))   //DA LI JE SUMA U OPSEGU
                        Console.Write("-1\t");
                    else
                        Console.Write(sume_kombinacija[i] + "\t");

                    if ((razlike_kombinacija[i] > razlika_max) || (razlike_kombinacija[i] < razlika_min))   //RAZLIKA NAJMANJEG I NAJVECEG
                        Console.Write("-1\n");
                    else
                        Console.Write(razlike_kombinacija[i] + "\n");

                    if (broj_parnih_brojeva_kombinacija[i] < 0)   //BROJ PARNIH
                        Console.Write("-1\n");
                    else
                        Console.Write(broj_parnih_brojeva_kombinacija[i] + "\n");

                    if (broj_parnih_brojeva_kombinacija[i] < 0)   //BROJ MALIH
                        Console.Write("-1\n");
                    else
                        Console.Write(broj_malih_brojeva_kombinacija[i] + "\n");

                    if (_indeks_zadnja_cifra(brojevi[i], duzina_kombinacije, zadnje_cifre[i]) == -1)   //ZADNJE CIFRE
                        Console.Write("-1\n");
                    else
                        Console.Write(_indeks_zadnja_cifra(brojevi[i], duzina_kombinacije, zadnje_cifre[i]) + "\n");

                    if (_indeks_susedni(brojevi[i], duzina_kombinacije) == -1)   //ZADNJE CIFRE
                        Console.Write("-1\n");
                    else
                        Console.Write(_indeks_susedni(brojevi[i], duzina_kombinacije) + "\n");

                    if (_indeks_skupovi_visak(brojevi[i], duzina_kombinacije, velicina_skupa) == -1)   //ZADNJE CIFRE
                        Console.Write("-1\n");
                    else
                        Console.Write(_indeks_susedni(brojevi[i], duzina_kombinacije) + "\n");*/
                }
            }
        }
    }
}