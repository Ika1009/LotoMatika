using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Xml;

namespace Loto_App
{
    public static class Combinations
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

        static int _indeks_zadnja_cifra_1(int[] niz, int duzina_niza)  //NALAZENJE CLANA NIZA SA ISTOM ZADNJOM CIFROM (1)
        {
            int[] zadnje_cifre = new int[10];
            for (int i = 0; i < 10; i++)
                zadnje_cifre[i] = 0;
            for (int i = 0; i < duzina_niza; i++)
                zadnje_cifre[niz[i] % 10]++;

            int par = -1;
            for (int i = 0; i < 10; i++)
            {
                if (zadnje_cifre[i] >= 2)
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

        static int _indeks_zadnja_cifra_2_plus(int[] niz, int duzina_niza)  //NALAZENJE CLANA NIZA SA ISTOM ZADNJOM CIFROM (2+)
        {
            int[] zadnje_cifre = new int[10];
            for (int i = 0; i < 10; i++)
                zadnje_cifre[i] = 0;
            for (int i = 0; i < duzina_niza; i++)
                zadnje_cifre[niz[i] % 10]++;

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

        static int _indeks_susedni_2_plus_para(int[] niz, int duzina_niza)  //NALAZENJE UZASTOPNOG CLANA NIZA (ZA 2+)
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

        static int _indeks_susedni_1_par(int[] niz, int duzina_niza)  //NALAZENJE UZASTOPNOG CLANA NIZA (ZA 1)
        {
            int[] sortiran_niz = new int[duzina_niza];
            for (int i = 0; i < duzina_niza; i++)
                sortiran_niz[i] = niz[i];
            Array.Sort(sortiran_niz);
            int par = -1;
            for (int i = 1; i < duzina_niza; i++)
            {
                if (sortiran_niz[i] == (sortiran_niz[i - 1] + 1))
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

        static int _broj_susednih(int[] niz, int duzina_niza)  //NALAZENJE BROJA SUSEDNIH NIZA
        {
            int[] sortiran_niz = new int[duzina_niza];
            for (int i = 0; i < duzina_niza; i++)
                sortiran_niz[i] = niz[i];
            Array.Sort(sortiran_niz);
            int broj_parova = 0;
            for (int i = 1; i < duzina_niza; i++)
                if (sortiran_niz[i] == (sortiran_niz[i - 1] + 1))
                    broj_parova++;

            return broj_parova;
        }

        static int _broj_zadnjih_cifara(int[] niz, int duzina_niza) //NALAZENJE BROJA ZADNJIH CIFARA
        {
            int[] zadnje_cifre = new int[10];
            for (int i = 0; i < 10; i++)
                zadnje_cifre[i] = 0;
            for (int i = 0; i < duzina_niza; i++)
                zadnje_cifre[niz[i] % 10]++;

            int broj_cifara = 0;
            for (int i = 0; i < 10; i++)
                if (zadnje_cifre[i] > 1)
                    broj_cifara += (zadnje_cifre[i] - 1);

            return broj_cifara;
        }

        static int _indeks_skupovi_visak_4(int[] niz, int duzina_niza, int velicina_skupa)  //NALAZENJE VISAK CLANA NIZA SKUPA (4)
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

        static int _indeks_skupovi_visak_5(int[] niz, int duzina_niza, int velicina_skupa)  //NALAZENJE VISAK CLANA NIZA SKUPA (5)
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
            if (broj_praznih_skupova <= 0)
                return -1;
            else if (broj_praznih_skupova > 0)
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

        static int _broj_susednih_elemenata(int[] niz, int duzina_niza, int element)
        {
            int broj_elemenata = 0;
            for (int i = 0; i < duzina_niza; i++)
                if ((niz[i] == (element - 1)) || (niz[i] == (element + 1)))
                    broj_elemenata++;

            return broj_elemenata;
        }

        static int _broj_zadnjih_cifara_elemenata(int[] niz, int duzina_niza, int element)
        {
            int broj_elemenata = 0;
            for (int i = 0; i < duzina_niza; i++)
                if ((niz[i] % 10) == (element % 10))
                    broj_elemenata++;

            return broj_elemenata;
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

        static int _broj_skupova(int[] niz, int duzina_niza, int velicina_skupa)  //NALAZENJE VISAK CLANA NIZA SKUPA (5)
        {
            int[] skupovi = new int[5];
            for (int i = 0; i < 5; i++)
                skupovi[i] = 0;
            int broj_skupova = 0;
            for (int i = 0; i < duzina_niza; i++)
                skupovi[(niz[i] - 1) / velicina_skupa]++;
            for (int i = 0; i < 5; i++)
                if (skupovi[i] > 0)
                    broj_skupova++;

            return broj_skupova;
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

        static void _resi_100_kombinacija(int[][] brojevi, int[] sume_kombinacija, int[] razlike_kombinacija, int pocetak, int kraj, int duzina_kombinacije, int broj_loptica, int granica_malih, int suma_min, int suma_max, int razlika_min, int razlika_max, int[] broj_parnih_brojeva_kombinacija, int[] broj_malih_brojeva_kombinacija, int velicina_skupa, bool[] petoskupovna_kombinacija, bool[] sa_susednima_kombinacija, bool[] sa_zadnjim_ciframa_kombinacija, int[] omiljeni_brojevi, int broj_omiljenih_brojeva)
        {
            Random random = new Random();
            int broj_kombinacija = kraj - pocetak + 1;

            int brojac_parni = 0;   //RESAVANJE PARNIH
            int broj_dobrih_parnih1 = 0;
            int broj_dobrih_parnih2 = 0;
            bool svi_parni_su_dobri = false;
            while (!svi_parni_su_dobri)
            {
                if (brojac_parni % 3 == 0)
                {
                    broj_dobrih_parnih1 = broj_dobrih_parnih2;
                    broj_dobrih_parnih2 = 0;
                    for (int i = (pocetak - 1); i < kraj; i++)
                        if (broj_parnih_brojeva_kombinacija[i] == _broj_parnih(brojevi[i], duzina_kombinacije))
                            broj_dobrih_parnih2++;
                    if (broj_dobrih_parnih2 <= broj_dobrih_parnih1)
                        break;
                }

                svi_parni_su_dobri = true;
                for (int i = (pocetak - 1); i < kraj; i++)
                {
                    if (broj_parnih_brojeva_kombinacija[i] < _broj_parnih(brojevi[i], duzina_kombinacije))
                    {
                        svi_parni_su_dobri = false;
                        for (int j = (pocetak - 1); (j < kraj) && (broj_parnih_brojeva_kombinacija[i] < _broj_parnih(brojevi[i], duzina_kombinacije)); j++)
                            if ((broj_parnih_brojeva_kombinacija[j] > _broj_parnih(brojevi[j], duzina_kombinacije))
                            && ((broj_omiljenih_brojeva == -1)
                            || ((broj_omiljenih_brojeva == 1) && (brojevi[i][_indeks_parni(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[j][_indeks_neparni(brojevi[j], duzina_kombinacije)] != omiljeni_brojevi[0]))
                            || ((broj_omiljenih_brojeva == 2) && (brojevi[i][_indeks_parni(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[j][_indeks_neparni(brojevi[j], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[i][_indeks_parni(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[1]) && (brojevi[j][_indeks_neparni(brojevi[j], duzina_kombinacije)] != omiljeni_brojevi[1]))))
                            {
                                int temp = brojevi[i][_indeks_parni(brojevi[i], duzina_kombinacije)];
                                brojevi[i][_indeks_parni(brojevi[i], duzina_kombinacije)] = brojevi[j][_indeks_neparni(brojevi[j], duzina_kombinacije)];
                                brojevi[j][_indeks_neparni(brojevi[j], duzina_kombinacije)] = temp;
                            }
                    }
                }

                brojac_parni++;
            }

            int brojac_mali = 0;    //RESAVANJE MALIH
            int broj_dobrih_malih1 = 0;
            int broj_dobrih_malih2 = 0;
            bool svi_mali_su_dobri = false;
            while (!svi_mali_su_dobri)
            {
                if (brojac_mali % 3 == 0)
                {
                    broj_dobrih_malih1 = broj_dobrih_malih2;
                    broj_dobrih_malih2 = 0;
                    for (int i = (pocetak - 1); i < kraj; i++)
                        if (broj_malih_brojeva_kombinacija[i] == _broj_malih(brojevi[i], duzina_kombinacije, granica_malih))
                            broj_dobrih_malih2++;
                    if (broj_dobrih_malih2 <= broj_dobrih_malih1)
                        break;
                }

                svi_mali_su_dobri = true;
                for (int i = (pocetak - 1); i < kraj; i++)
                {
                    if (broj_malih_brojeva_kombinacija[i] < _broj_malih(brojevi[i], duzina_kombinacije, granica_malih))
                    {
                        svi_mali_su_dobri = false;
                        for (int j = (pocetak - 1); (j < kraj) && (broj_malih_brojeva_kombinacija[i] < _broj_malih(brojevi[i], duzina_kombinacije, granica_malih)); j++)
                            if (broj_malih_brojeva_kombinacija[j] > _broj_malih(brojevi[j], duzina_kombinacije, granica_malih)
                            && ((brojevi[i][_indeks_mali(brojevi[i], duzina_kombinacije, granica_malih)] % 2) == (brojevi[j][_indeks_veliki(brojevi[j], duzina_kombinacije, granica_malih)] % 2))
                            && ((broj_omiljenih_brojeva == -1)
                            || ((broj_omiljenih_brojeva == 1) && (brojevi[i][_indeks_mali(brojevi[i], duzina_kombinacije, granica_malih)] != omiljeni_brojevi[0]) && (brojevi[j][_indeks_veliki(brojevi[j], duzina_kombinacije, granica_malih)] != omiljeni_brojevi[0]))
                            || ((broj_omiljenih_brojeva == 2) && (brojevi[i][_indeks_mali(brojevi[i], duzina_kombinacije, granica_malih)] != omiljeni_brojevi[0]) && (brojevi[j][_indeks_veliki(brojevi[j], duzina_kombinacije, granica_malih)] != omiljeni_brojevi[0]) && (brojevi[i][_indeks_mali(brojevi[i], duzina_kombinacije, granica_malih)] != omiljeni_brojevi[1]) && (brojevi[j][_indeks_veliki(brojevi[j], duzina_kombinacije, granica_malih)] != omiljeni_brojevi[1]))))
                            {
                                int temp = brojevi[i][_indeks_mali(brojevi[i], duzina_kombinacije, granica_malih)];
                                brojevi[i][_indeks_mali(brojevi[i], duzina_kombinacije, granica_malih)] = brojevi[j][_indeks_veliki(brojevi[j], duzina_kombinacije, granica_malih)];
                                brojevi[j][_indeks_veliki(brojevi[j], duzina_kombinacije, granica_malih)] = temp;
                            }
                    }
                }

                brojac_mali++;
            }

            int brojac_dupli = 0;   //RESAVANJE DUPLIH
            int broj_dobrih_duplih1 = -1;
            int broj_dobrih_duplih2 = 0;
            bool svi_duplikati_su_dobri = false;
            while (!svi_duplikati_su_dobri)
            {
                if (brojac_dupli % 10 == 0)
                {
                    broj_dobrih_duplih1 = broj_dobrih_duplih2;
                    broj_dobrih_duplih2 = 0;
                    for (int i = (pocetak - 1); i < kraj; i++)
                        if (_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica) == -1)
                            broj_dobrih_duplih2++;
                    if ((broj_dobrih_duplih2 <= broj_dobrih_duplih1))
                        break;
                }

                svi_duplikati_su_dobri = true;
                for (int i = (pocetak - 1); i < kraj; i++)
                {
                    if (_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica) != -1)
                    {
                        svi_duplikati_su_dobri = false;
                        for (int j = (i + 1); (j < kraj) && (_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica) != -1); j++)
                            for (int k = 0; (k < duzina_kombinacije) && (_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica) != -1); k++)
                                if (!_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k])
                                && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)])
                                && ((brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)] % 2) == (brojevi[j][k] % 2))
                                && (((brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)] <= granica_malih) && (brojevi[j][k] <= granica_malih)) || ((brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)] > granica_malih) && (brojevi[j][k] > granica_malih)))
                                && ((broj_omiljenih_brojeva == -1)
                                || ((broj_omiljenih_brojeva == 1) && (brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]))
                                || ((broj_omiljenih_brojeva == 2) && (brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]) && (brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)] != omiljeni_brojevi[1]) && (brojevi[j][k] != omiljeni_brojevi[1]))))
                                {
                                    int temp = brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)];
                                    brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)] = brojevi[j][k];
                                    brojevi[j][k] = temp;
                                }
                    }
                }

                brojac_dupli++;
            }
            brojac_dupli = 1;
            broj_dobrih_duplih1 = 0;
            broj_dobrih_duplih2 = -1;
            while (!svi_duplikati_su_dobri)
            {
                if (brojac_dupli % 10 == 0)
                {
                    broj_dobrih_duplih1 = broj_dobrih_duplih2;
                    broj_dobrih_duplih2 = 0;
                    for (int i = (pocetak - 1); i < kraj; i++)
                        if (_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica) == -1)
                            broj_dobrih_duplih2++;
                    if (broj_dobrih_duplih2 <= broj_dobrih_duplih1)
                        break;
                }

                svi_duplikati_su_dobri = true;
                for (int i = (pocetak - 1); i < kraj; i++)
                {
                    if (_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica) != -1)
                    {
                        svi_duplikati_su_dobri = false;
                        for (int j = (i + 1); (j < kraj) && (_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica) != -1); j++)
                            for (int k = 0; (k < duzina_kombinacije) && (_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica) != -1); k++)
                                if (!_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k])
                                && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)])
                                && ((broj_omiljenih_brojeva == -1)
                                || ((broj_omiljenih_brojeva == 1) && (brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]))
                                || ((broj_omiljenih_brojeva == 2) && (brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]) && (brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)] != omiljeni_brojevi[1]) && (brojevi[j][k] != omiljeni_brojevi[1]))))
                                {
                                    int temp = brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)];
                                    brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)] = brojevi[j][k];
                                    brojevi[j][k] = temp;
                                }
                    }
                }

                brojac_dupli++;
            }

            int brojac_skupovi = 0;   //STVARANJE SKUPOVA
            int broj_dobrih_skupova1 = 0;
            int broj_dobrih_skupova2 = 0;
            bool svi_skupovi_su_dobri = false;
            int nasumicni_element;
            int[] nova_kombinacija1 = new int[duzina_kombinacije];
            int[] nova_kombinacija2 = new int[duzina_kombinacije];
            int odrzan_broj_kombinacija_skupovi;
            while (!svi_skupovi_su_dobri)
            {
                if (brojac_skupovi % 5 == 0)
                {
                    broj_dobrih_skupova1 = broj_dobrih_skupova2;
                    broj_dobrih_skupova2 = 0;
                    for (int i = (pocetak - 1); i < kraj; i++)
                        if (!petoskupovna_kombinacija[i] && (_indeks_skupovi_visak_4(brojevi[i], duzina_kombinacije, velicina_skupa) == -1))
                            broj_dobrih_skupova2++;
                        else if (petoskupovna_kombinacija[i] && (_indeks_skupovi_visak_5(brojevi[i], duzina_kombinacije, velicina_skupa) == -1))
                            broj_dobrih_skupova2++;
                    if (broj_dobrih_skupova2 <= broj_dobrih_skupova1)
                        break;
                }

                svi_skupovi_su_dobri = true;
                for (int i = 0; i < broj_kombinacija; i++)
                {
                    if (!petoskupovna_kombinacija[i])   //CETVOROSKUPNI
                    {
                        if (_broj_skupova(brojevi[i], duzina_kombinacije, velicina_skupa) > 4)  //UKLONI SKUP
                        {
                            svi_skupovi_su_dobri = false;
                            for (int j = (pocetak - 1); (j < kraj) && (_broj_skupova(brojevi[i], duzina_kombinacije, velicina_skupa) > 4); j++)
                                for (int k = 0; (k < duzina_kombinacije) && (_broj_skupova(brojevi[i], duzina_kombinacije, velicina_skupa) > 4); k++)
                                {
                                    nasumicni_element = random.Next(0, duzina_kombinacije);
                                    for (int l = 0; l < duzina_kombinacije; l++)
                                    {
                                        nova_kombinacija1[l] = brojevi[i][l];
                                        nova_kombinacija2[l] = brojevi[j][l];
                                    }
                                    nova_kombinacija1[nasumicni_element] = brojevi[j][k];
                                    nova_kombinacija2[k] = brojevi[i][nasumicni_element];
                                    if (petoskupovna_kombinacija[j])
                                        odrzan_broj_kombinacija_skupovi = 5;
                                    else
                                        odrzan_broj_kombinacija_skupovi = 4;
                                    if ((_broj_skupova(brojevi[i], duzina_kombinacije, velicina_skupa) >= _broj_skupova(nova_kombinacija1, duzina_kombinacije, velicina_skupa))
                                    && (Math.Abs(_broj_skupova(brojevi[j], duzina_kombinacije, velicina_skupa) - odrzan_broj_kombinacija_skupovi) >= Math.Abs(_broj_skupova(nova_kombinacija2, duzina_kombinacije, velicina_skupa) - odrzan_broj_kombinacija_skupovi))
                                    && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][nasumicni_element])
                                    && !_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k])
                                    && ((brojevi[i][nasumicni_element] % 2) == (brojevi[j][k] % 2))
                                    && (((brojevi[i][nasumicni_element] <= granica_malih) && (brojevi[j][k] <= granica_malih)) || ((brojevi[i][nasumicni_element] > granica_malih) && (brojevi[j][k] > granica_malih)))
                                    && ((broj_omiljenih_brojeva == -1)
                                    || ((broj_omiljenih_brojeva == 1) && (brojevi[i][nasumicni_element] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]))
                                    || ((broj_omiljenih_brojeva == 2) && (brojevi[i][nasumicni_element] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]) && (brojevi[i][nasumicni_element] != omiljeni_brojevi[1]) && (brojevi[j][k] != omiljeni_brojevi[1]))))
                                    {
                                        int temp = brojevi[i][nasumicni_element];
                                        brojevi[i][nasumicni_element] = brojevi[j][k];
                                        brojevi[j][k] = temp;
                                    }
                                }
                        }
                        else if (_broj_skupova(brojevi[i], duzina_kombinacije, velicina_skupa) < 4) //DODAJ SKUP
                        {

                            svi_skupovi_su_dobri = false;
                            for (int j = (pocetak - 1); (j < kraj) && (_broj_skupova(brojevi[i], duzina_kombinacije, velicina_skupa) < 4); j++)
                                for (int k = 0; (k < duzina_kombinacije) && (_broj_skupova(brojevi[i], duzina_kombinacije, velicina_skupa) < 4); k++)
                                {
                                    for (int l = 0; l < duzina_kombinacije; l++)
                                    {
                                        nova_kombinacija1[l] = brojevi[i][l];
                                        nova_kombinacija2[l] = brojevi[j][l];
                                    }
                                    nova_kombinacija1[_indeks_skupovi_visak_4(brojevi[i], duzina_kombinacije, velicina_skupa)] = brojevi[j][k];
                                    nova_kombinacija2[k] = brojevi[i][_indeks_skupovi_visak_4(brojevi[i], duzina_kombinacije, velicina_skupa)];
                                    if (petoskupovna_kombinacija[j])
                                        odrzan_broj_kombinacija_skupovi = 5;
                                    else
                                        odrzan_broj_kombinacija_skupovi = 4;
                                    if ((Math.Abs(_broj_skupova(brojevi[j], duzina_kombinacije, velicina_skupa) - odrzan_broj_kombinacija_skupovi) >= Math.Abs(_broj_skupova(nova_kombinacija2, duzina_kombinacije, velicina_skupa) - odrzan_broj_kombinacija_skupovi))
                                    && (_broj_skupova(brojevi[i], duzina_kombinacije, velicina_skupa) <= _broj_skupova(nova_kombinacija1, duzina_kombinacije, velicina_skupa))
                                    && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][_indeks_skupovi_visak_4(brojevi[i], duzina_kombinacije, velicina_skupa)])
                                    && !_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k])
                                    && ((brojevi[i][_indeks_skupovi_visak_4(brojevi[i], duzina_kombinacije, velicina_skupa)] % 2) == (brojevi[j][k] % 2))
                                    && (((brojevi[i][_indeks_skupovi_visak_4(brojevi[i], duzina_kombinacije, velicina_skupa)] <= granica_malih) && (brojevi[j][k] <= granica_malih)) || ((brojevi[i][_indeks_skupovi_visak_4(brojevi[i], duzina_kombinacije, velicina_skupa)] > granica_malih) && (brojevi[j][k] > granica_malih)))
                                    && ((broj_omiljenih_brojeva == -1)
                                    || ((broj_omiljenih_brojeva == 1) && (brojevi[i][_indeks_skupovi_visak_4(brojevi[i], duzina_kombinacije, velicina_skupa)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]))
                                    || ((broj_omiljenih_brojeva == 2) && (brojevi[i][_indeks_skupovi_visak_4(brojevi[i], duzina_kombinacije, velicina_skupa)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]) && (brojevi[i][_indeks_skupovi_visak_4(brojevi[i], duzina_kombinacije, velicina_skupa)] != omiljeni_brojevi[1]) && (brojevi[j][k] != omiljeni_brojevi[1]))))
                                    {
                                        int temp = brojevi[i][_indeks_skupovi_visak_4(brojevi[i], duzina_kombinacije, velicina_skupa)];
                                        brojevi[i][_indeks_skupovi_visak_4(brojevi[i], duzina_kombinacije, velicina_skupa)] = brojevi[j][k];
                                        brojevi[j][k] = temp;
                                    }
                                }
                        }
                    }
                    else if (petoskupovna_kombinacija[i])   //PETOSKUPNI
                    {
                        if (_broj_skupova(brojevi[i], duzina_kombinacije, velicina_skupa) < 5)  //DODAJ SKUP
                        {
                            svi_skupovi_su_dobri = false;
                            for (int j = (pocetak - 1); (j < kraj) && (_broj_skupova(brojevi[i], duzina_kombinacije, velicina_skupa) < 5); j++)
                                for (int k = 0; (k < duzina_kombinacije) && (_broj_skupova(brojevi[i], duzina_kombinacije, velicina_skupa) < 5); k++)
                                {
                                    for (int l = 0; l < duzina_kombinacije; l++)
                                    {
                                        nova_kombinacija1[l] = brojevi[i][l];
                                        nova_kombinacija2[l] = brojevi[j][l];
                                    }
                                    nova_kombinacija1[_indeks_skupovi_visak_5(brojevi[i], duzina_kombinacije, velicina_skupa)] = brojevi[j][k];
                                    nova_kombinacija2[k] = brojevi[i][_indeks_skupovi_visak_5(brojevi[i], duzina_kombinacije, velicina_skupa)];
                                    if (petoskupovna_kombinacija[j])
                                        odrzan_broj_kombinacija_skupovi = 5;
                                    else
                                        odrzan_broj_kombinacija_skupovi = 4;
                                    if ((Math.Abs(_broj_skupova(brojevi[j], duzina_kombinacije, velicina_skupa) - odrzan_broj_kombinacija_skupovi) >= Math.Abs(_broj_skupova(nova_kombinacija2, duzina_kombinacije, velicina_skupa) - odrzan_broj_kombinacija_skupovi))
                                    && (_broj_skupova(brojevi[i], duzina_kombinacije, velicina_skupa) <= _broj_skupova(nova_kombinacija1, duzina_kombinacije, velicina_skupa))
                                    && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][_indeks_skupovi_visak_5(brojevi[i], duzina_kombinacije, velicina_skupa)])
                                    && !_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k])
                                    && ((brojevi[i][_indeks_skupovi_visak_5(brojevi[i], duzina_kombinacije, velicina_skupa)] % 2) == (brojevi[j][k] % 2))
                                    && (((brojevi[i][_indeks_skupovi_visak_5(brojevi[i], duzina_kombinacije, velicina_skupa)] <= granica_malih) && (brojevi[j][k] <= granica_malih)) || ((brojevi[i][_indeks_skupovi_visak_5(brojevi[i], duzina_kombinacije, velicina_skupa)] > granica_malih) && (brojevi[j][k] > granica_malih)))
                                    && ((broj_omiljenih_brojeva == -1)
                                    || ((broj_omiljenih_brojeva == 1) && (brojevi[i][_indeks_skupovi_visak_5(brojevi[i], duzina_kombinacije, velicina_skupa)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]))
                                    || ((broj_omiljenih_brojeva == 2) && (brojevi[i][_indeks_skupovi_visak_5(brojevi[i], duzina_kombinacije, velicina_skupa)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]) && (brojevi[i][_indeks_skupovi_visak_5(brojevi[i], duzina_kombinacije, velicina_skupa)] != omiljeni_brojevi[1]) && (brojevi[j][k] != omiljeni_brojevi[1]))))
                                    {
                                        int temp = brojevi[i][_indeks_skupovi_visak_5(brojevi[i], duzina_kombinacije, velicina_skupa)];
                                        brojevi[i][_indeks_skupovi_visak_5(brojevi[i], duzina_kombinacije, velicina_skupa)] = brojevi[j][k];
                                        brojevi[j][k] = temp;
                                    }
                                }
                        }
                    }
                }

                brojac_skupovi++;
            }

            int brojac_susedni = 0;   //STVARANJE SUSEDNIH
            int broj_dobrih_susednih1 = 0;
            int broj_dobrih_susednih2 = 0;
            bool svi_susedni_su_dobri = false;
            int odrzan_broj_kombinacija_susedni;
            while (!svi_susedni_su_dobri)
            {
                if (brojac_susedni % 5 == 0)
                {
                    broj_dobrih_susednih1 = broj_dobrih_susednih2;
                    broj_dobrih_susednih2 = 0;
                    for (int i = (pocetak - 1); i < kraj; i++)
                        if (sa_susednima_kombinacija[i] && (_indeks_susedni_2_plus_para(brojevi[i], duzina_kombinacije) == -1) && (_indeks_susedni_1_par(brojevi[i], duzina_kombinacije) != -1))
                            broj_dobrih_susednih2++;
                        else if (!sa_susednima_kombinacija[i] && (_indeks_susedni_1_par(brojevi[i], duzina_kombinacije) == -1))
                            broj_dobrih_susednih2++;
                    if (broj_dobrih_susednih2 <= broj_dobrih_susednih1)
                        break;
                }

                svi_susedni_su_dobri = true;
                for (int i = (pocetak - 1); i < kraj; i++)
                {
                    if (sa_susednima_kombinacija[i])   //JEDAN SUSEDNI
                    {
                        if (_indeks_susedni_1_par(brojevi[i], duzina_kombinacije) == -1)  //DODAJ SUSEDNOG
                        {
                            svi_susedni_su_dobri = false;
                            for (int j = (pocetak - 1); (j < kraj) && (_indeks_susedni_1_par(brojevi[i], duzina_kombinacije) == -1); j++)
                                for (int k = 0; (k < duzina_kombinacije) && (_indeks_susedni_1_par(brojevi[i], duzina_kombinacije) == -1); k++)
                                {
                                    nasumicni_element = random.Next(0, duzina_kombinacije);
                                    for (int l = 0; l < duzina_kombinacije; l++)
                                    {
                                        nova_kombinacija1[l] = brojevi[i][l];
                                        nova_kombinacija2[l] = brojevi[j][l];
                                    }
                                    nova_kombinacija1[nasumicni_element] = brojevi[j][k];
                                    nova_kombinacija2[k] = brojevi[i][nasumicni_element];
                                    if (sa_susednima_kombinacija[j])
                                        odrzan_broj_kombinacija_susedni = 1;
                                    else
                                        odrzan_broj_kombinacija_susedni = 0;
                                    if (petoskupovna_kombinacija[j])
                                        odrzan_broj_kombinacija_skupovi = 5;
                                    else
                                        odrzan_broj_kombinacija_skupovi = 4;
                                    if ((_broj_susednih(brojevi[i], duzina_kombinacije) <= _broj_susednih(nova_kombinacija1, duzina_kombinacije))
                                    && (Math.Abs(_broj_susednih(brojevi[j], duzina_kombinacije) - odrzan_broj_kombinacija_susedni) >= Math.Abs(_broj_susednih(nova_kombinacija2, duzina_kombinacije) - odrzan_broj_kombinacija_susedni))
                                    && (Math.Abs(_broj_skupova(brojevi[j], duzina_kombinacije, velicina_skupa) - odrzan_broj_kombinacija_skupovi) >= Math.Abs(_broj_skupova(nova_kombinacija2, duzina_kombinacije, velicina_skupa) - odrzan_broj_kombinacija_skupovi))
                                    && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][nasumicni_element])
                                    && !_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k])
                                    && ((brojevi[i][nasumicni_element] % 2) == (brojevi[j][k] % 2))
                                    && (((brojevi[i][nasumicni_element] <= granica_malih) && (brojevi[j][k] <= granica_malih)) || ((brojevi[i][nasumicni_element] > granica_malih) && (brojevi[j][k] > granica_malih)))
                                    && ((broj_omiljenih_brojeva == -1)
                                    || ((broj_omiljenih_brojeva == 1) && (brojevi[i][nasumicni_element] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]))
                                    || ((broj_omiljenih_brojeva == 2) && (brojevi[i][nasumicni_element] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]) && (brojevi[i][nasumicni_element] != omiljeni_brojevi[1]) && (brojevi[j][k] != omiljeni_brojevi[1]))))
                                    {
                                        int temp = brojevi[i][nasumicni_element];
                                        brojevi[i][nasumicni_element] = brojevi[j][k];
                                        brojevi[j][k] = temp;
                                    }
                                }
                        }
                        else if (_indeks_susedni_2_plus_para(brojevi[i], duzina_kombinacije) != -1) //UKLONI SUSEDNOG
                        {

                            svi_susedni_su_dobri = false;
                            for (int j = (pocetak - 1); (j < kraj) && (_indeks_susedni_2_plus_para(brojevi[i], duzina_kombinacije) != -1); j++)
                                for (int k = 0; (k < duzina_kombinacije) && (_indeks_susedni_2_plus_para(brojevi[i], duzina_kombinacije) != -1); k++)
                                {
                                    for (int l = 0; l < duzina_kombinacije; l++)
                                    {
                                        nova_kombinacija1[l] = brojevi[i][l];
                                        nova_kombinacija2[l] = brojevi[j][l];
                                    }
                                    nova_kombinacija1[_indeks_susedni_2_plus_para(brojevi[i], duzina_kombinacije)] = brojevi[j][k];
                                    nova_kombinacija2[k] = brojevi[i][_indeks_susedni_2_plus_para(brojevi[i], duzina_kombinacije)];
                                    if (sa_susednima_kombinacija[j])
                                        odrzan_broj_kombinacija_susedni = 1;
                                    else
                                        odrzan_broj_kombinacija_susedni = 0;
                                    if (petoskupovna_kombinacija[j])
                                        odrzan_broj_kombinacija_skupovi = 5;
                                    else
                                        odrzan_broj_kombinacija_skupovi = 4;
                                    if ((_broj_susednih(brojevi[i], duzina_kombinacije) >= _broj_susednih(nova_kombinacija1, duzina_kombinacije))
                                    && (Math.Abs(_broj_susednih(brojevi[j], duzina_kombinacije) - odrzan_broj_kombinacija_susedni) >= Math.Abs(_broj_susednih(nova_kombinacija2, duzina_kombinacije) - odrzan_broj_kombinacija_susedni))
                                    && (Math.Abs(_broj_skupova(brojevi[j], duzina_kombinacije, velicina_skupa) - odrzan_broj_kombinacija_skupovi) >= Math.Abs(_broj_skupova(nova_kombinacija2, duzina_kombinacije, velicina_skupa) - odrzan_broj_kombinacija_skupovi))
                                    && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][_indeks_susedni_2_plus_para(brojevi[i], duzina_kombinacije)])
                                    && !_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k])
                                    && ((brojevi[i][_indeks_susedni_2_plus_para(brojevi[i], duzina_kombinacije)] % 2) == (brojevi[j][k] % 2))
                                    && (((brojevi[i][_indeks_susedni_2_plus_para(brojevi[i], duzina_kombinacije)] <= granica_malih) && (brojevi[j][k] <= granica_malih)) || ((brojevi[i][_indeks_susedni_2_plus_para(brojevi[i], duzina_kombinacije)] > granica_malih) && (brojevi[j][k] > granica_malih)))
                                    && ((broj_omiljenih_brojeva == -1)
                                    || ((broj_omiljenih_brojeva == 1) && (brojevi[i][_indeks_susedni_2_plus_para(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]))
                                    || ((broj_omiljenih_brojeva == 2) && (brojevi[i][_indeks_susedni_2_plus_para(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]) && (brojevi[i][_indeks_susedni_2_plus_para(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[1]) && (brojevi[j][k] != omiljeni_brojevi[1]))))
                                    {
                                        int temp = brojevi[i][_indeks_susedni_2_plus_para(brojevi[i], duzina_kombinacije)];
                                        brojevi[i][_indeks_susedni_2_plus_para(brojevi[i], duzina_kombinacije)] = brojevi[j][k];
                                        brojevi[j][k] = temp;
                                    }
                                }
                        }
                    }
                    else if (!sa_susednima_kombinacija[i])   //NULA SUSEDNIH
                    {
                        if (_indeks_susedni_1_par(brojevi[i], duzina_kombinacije) != -1)  //UKLONI SUSEDNOG
                        {
                            svi_susedni_su_dobri = false;
                            for (int j = (pocetak - 1); (j < kraj) && (_indeks_susedni_1_par(brojevi[i], duzina_kombinacije) != -1); j++)
                                for (int k = 0; (k < duzina_kombinacije) && (_indeks_susedni_1_par(brojevi[i], duzina_kombinacije) != -1); k++)
                                {
                                    for (int l = 0; l < duzina_kombinacije; l++)
                                    {
                                        nova_kombinacija1[l] = brojevi[i][l];
                                        nova_kombinacija2[l] = brojevi[j][l];
                                    }
                                    nova_kombinacija1[_indeks_susedni_1_par(brojevi[i], duzina_kombinacije)] = brojevi[j][k];
                                    nova_kombinacija2[k] = brojevi[i][_indeks_susedni_1_par(brojevi[i], duzina_kombinacije)];
                                    if (sa_susednima_kombinacija[j])
                                        odrzan_broj_kombinacija_susedni = 1;
                                    else
                                        odrzan_broj_kombinacija_susedni = 0;
                                    if (petoskupovna_kombinacija[j])
                                        odrzan_broj_kombinacija_skupovi = 5;
                                    else
                                        odrzan_broj_kombinacija_skupovi = 4;
                                    if ((_broj_susednih(brojevi[i], duzina_kombinacije) >= _broj_susednih(nova_kombinacija1, duzina_kombinacije))
                                    && (Math.Abs(_broj_susednih(brojevi[j], duzina_kombinacije) - odrzan_broj_kombinacija_susedni) >= Math.Abs(_broj_susednih(nova_kombinacija2, duzina_kombinacije) - odrzan_broj_kombinacija_susedni))
                                    && (Math.Abs(_broj_skupova(brojevi[j], duzina_kombinacije, velicina_skupa) - odrzan_broj_kombinacija_skupovi) >= Math.Abs(_broj_skupova(nova_kombinacija2, duzina_kombinacije, velicina_skupa) - odrzan_broj_kombinacija_skupovi))
                                    && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][_indeks_susedni_1_par(brojevi[i], duzina_kombinacije)])
                                    && !_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k])
                                    && ((brojevi[i][_indeks_susedni_1_par(brojevi[i], duzina_kombinacije)] % 2) == (brojevi[j][k] % 2))
                                    && (((brojevi[i][_indeks_susedni_1_par(brojevi[i], duzina_kombinacije)] <= granica_malih) && (brojevi[j][k] <= granica_malih)) || ((brojevi[i][_indeks_susedni_1_par(brojevi[i], duzina_kombinacije)] > granica_malih) && (brojevi[j][k] > granica_malih)))
                                    && ((broj_omiljenih_brojeva == -1)
                                    || ((broj_omiljenih_brojeva == 1) && (brojevi[i][_indeks_susedni_1_par(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]))
                                    || ((broj_omiljenih_brojeva == 2) && (brojevi[i][_indeks_susedni_1_par(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]) && (brojevi[i][_indeks_susedni_1_par(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[1]) && (brojevi[j][k] != omiljeni_brojevi[1]))))
                                    {
                                        int temp = brojevi[i][_indeks_susedni_1_par(brojevi[i], duzina_kombinacije)];
                                        brojevi[i][_indeks_susedni_1_par(brojevi[i], duzina_kombinacije)] = brojevi[j][k];
                                        brojevi[j][k] = temp;
                                    }
                                }
                        }
                    }
                }

                brojac_susedni++;
            }

            int brojac_cifre = 0;   //STVARANJE ZADNJIH CIFARA
            int broj_dobrih_cifara1 = 0;
            int broj_dobrih_cifara2 = 0;
            bool sve_cifre_su_dobre = false;
            int odrzan_broj_kombinacija_cifre;
            while (!sve_cifre_su_dobre)
            {
                if (brojac_cifre % 5 == 0)
                {
                    broj_dobrih_cifara1 = broj_dobrih_cifara2;
                    broj_dobrih_cifara2 = 0;
                    for (int i = (pocetak - 1); i < kraj; i++)
                        if (sa_zadnjim_ciframa_kombinacija[i] && (_indeks_zadnja_cifra_2_plus(brojevi[i], duzina_kombinacije) == -1) && (_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije) != -1))
                            broj_dobrih_cifara2++;
                        else if (!sa_zadnjim_ciframa_kombinacija[i] && (_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije) == -1))
                            broj_dobrih_cifara2++;
                    if (broj_dobrih_cifara2 <= broj_dobrih_cifara1)
                        break;
                }

                sve_cifre_su_dobre = true;
                for (int i = (pocetak - 1); i < kraj; i++)
                {
                    if (sa_zadnjim_ciframa_kombinacija[i])   //JEDNA CIFRA
                    {
                        if (_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije) == -1)  //DODAJ CIFRU
                        {
                            sve_cifre_su_dobre = false;
                            for (int j = (pocetak - 1); (j < kraj) && (_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije) == -1); j++)
                                for (int k = 0; (k < duzina_kombinacije) && (_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije) == -1); k++)
                                {
                                    nasumicni_element = random.Next(0, duzina_kombinacije);
                                    for (int l = 0; l < duzina_kombinacije; l++)
                                    {
                                        nova_kombinacija1[l] = brojevi[i][l];
                                        nova_kombinacija2[l] = brojevi[j][l];
                                    }
                                    nova_kombinacija1[nasumicni_element] = brojevi[j][k];
                                    nova_kombinacija2[k] = brojevi[i][nasumicni_element];
                                    if (sa_zadnjim_ciframa_kombinacija[j])
                                        odrzan_broj_kombinacija_cifre = 1;
                                    else
                                        odrzan_broj_kombinacija_cifre = 0;
                                    if (sa_susednima_kombinacija[j])
                                        odrzan_broj_kombinacija_susedni = 1;
                                    else
                                        odrzan_broj_kombinacija_susedni = 0;
                                    if (petoskupovna_kombinacija[j])
                                        odrzan_broj_kombinacija_skupovi = 5;
                                    else
                                        odrzan_broj_kombinacija_skupovi = 4;
                                    if ((_broj_zadnjih_cifara(brojevi[i], duzina_kombinacije) <= _broj_zadnjih_cifara(nova_kombinacija1, duzina_kombinacije))
                                    && (Math.Abs(_broj_zadnjih_cifara(brojevi[j], duzina_kombinacije) - odrzan_broj_kombinacija_cifre) >= Math.Abs(_broj_zadnjih_cifara(nova_kombinacija2, duzina_kombinacije) - odrzan_broj_kombinacija_cifre))
                                    && (Math.Abs(_broj_susednih(brojevi[j], duzina_kombinacije) - odrzan_broj_kombinacija_susedni) >= Math.Abs(_broj_susednih(nova_kombinacija2, duzina_kombinacije) - odrzan_broj_kombinacija_susedni))
                                    && (Math.Abs(_broj_skupova(brojevi[j], duzina_kombinacije, velicina_skupa) - odrzan_broj_kombinacija_skupovi) >= Math.Abs(_broj_skupova(nova_kombinacija2, duzina_kombinacije, velicina_skupa) - odrzan_broj_kombinacija_skupovi))
                                    && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][nasumicni_element])
                                    && !_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k])
                                    && ((brojevi[i][nasumicni_element] % 2) == (brojevi[j][k] % 2))
                                    && (((brojevi[i][nasumicni_element] <= granica_malih) && (brojevi[j][k] <= granica_malih)) || ((brojevi[i][nasumicni_element] > granica_malih) && (brojevi[j][k] > granica_malih)))
                                    && ((broj_omiljenih_brojeva == -1)
                                    || ((broj_omiljenih_brojeva == 1) && (brojevi[i][nasumicni_element] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]))
                                    || ((broj_omiljenih_brojeva == 2) && (brojevi[i][nasumicni_element] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]) && (brojevi[i][nasumicni_element] != omiljeni_brojevi[1]) && (brojevi[j][k] != omiljeni_brojevi[1]))))
                                    {
                                        int temp = brojevi[i][nasumicni_element];
                                        brojevi[i][nasumicni_element] = brojevi[j][k];
                                        brojevi[j][k] = temp;
                                    }
                                }
                        }
                        else if (_indeks_zadnja_cifra_2_plus(brojevi[i], duzina_kombinacije) != -1) //UKLONI CIFRU
                        {

                            sve_cifre_su_dobre = false;
                            for (int j = (pocetak - 1); (j < kraj) && (_indeks_zadnja_cifra_2_plus(brojevi[i], duzina_kombinacije) != -1); j++)
                                for (int k = 0; (k < duzina_kombinacije) && (_indeks_zadnja_cifra_2_plus(brojevi[i], duzina_kombinacije) != -1); k++)
                                {
                                    for (int l = 0; l < duzina_kombinacije; l++)
                                    {
                                        nova_kombinacija1[l] = brojevi[i][l];
                                        nova_kombinacija2[l] = brojevi[j][l];
                                    }
                                    nova_kombinacija1[_indeks_zadnja_cifra_2_plus(brojevi[i], duzina_kombinacije)] = brojevi[j][k];
                                    nova_kombinacija2[k] = brojevi[i][_indeks_zadnja_cifra_2_plus(brojevi[i], duzina_kombinacije)];
                                    if (sa_zadnjim_ciframa_kombinacija[j])
                                        odrzan_broj_kombinacija_cifre = 1;
                                    else
                                        odrzan_broj_kombinacija_cifre = 0;
                                    if (sa_susednima_kombinacija[j])
                                        odrzan_broj_kombinacija_susedni = 1;
                                    else
                                        odrzan_broj_kombinacija_susedni = 0;
                                    if (petoskupovna_kombinacija[j])
                                        odrzan_broj_kombinacija_skupovi = 5;
                                    else
                                        odrzan_broj_kombinacija_skupovi = 4;
                                    if ((_broj_zadnjih_cifara(brojevi[i], duzina_kombinacije) >= _broj_zadnjih_cifara(nova_kombinacija1, duzina_kombinacije))
                                    && (Math.Abs(_broj_zadnjih_cifara(brojevi[j], duzina_kombinacije) - odrzan_broj_kombinacija_cifre) >= Math.Abs(_broj_zadnjih_cifara(nova_kombinacija2, duzina_kombinacije) - odrzan_broj_kombinacija_cifre))
                                    && (Math.Abs(_broj_susednih(brojevi[j], duzina_kombinacije) - odrzan_broj_kombinacija_susedni) >= Math.Abs(_broj_susednih(nova_kombinacija2, duzina_kombinacije) - odrzan_broj_kombinacija_susedni))
                                    && (Math.Abs(_broj_skupova(brojevi[j], duzina_kombinacije, velicina_skupa) - odrzan_broj_kombinacija_skupovi) >= Math.Abs(_broj_skupova(nova_kombinacija2, duzina_kombinacije, velicina_skupa) - odrzan_broj_kombinacija_skupovi))
                                    && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][_indeks_zadnja_cifra_2_plus(brojevi[i], duzina_kombinacije)])
                                    && !_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k])
                                    && ((brojevi[i][_indeks_zadnja_cifra_2_plus(brojevi[i], duzina_kombinacije)] % 2) == (brojevi[j][k] % 2))
                                    && (((brojevi[i][_indeks_zadnja_cifra_2_plus(brojevi[i], duzina_kombinacije)] <= granica_malih) && (brojevi[j][k] <= granica_malih)) || ((brojevi[i][_indeks_zadnja_cifra_2_plus(brojevi[i], duzina_kombinacije)] > granica_malih) && (brojevi[j][k] > granica_malih)))
                                    && ((broj_omiljenih_brojeva == -1)
                                    || ((broj_omiljenih_brojeva == 1) && (brojevi[i][_indeks_zadnja_cifra_2_plus(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]))
                                    || ((broj_omiljenih_brojeva == 2) && (brojevi[i][_indeks_zadnja_cifra_2_plus(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]) && (brojevi[i][_indeks_zadnja_cifra_2_plus(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[1]) && (brojevi[j][k] != omiljeni_brojevi[1]))))
                                    {
                                        int temp = brojevi[i][_indeks_zadnja_cifra_2_plus(brojevi[i], duzina_kombinacije)];
                                        brojevi[i][_indeks_zadnja_cifra_2_plus(brojevi[i], duzina_kombinacije)] = brojevi[j][k];
                                        brojevi[j][k] = temp;
                                    }
                                }
                        }
                    }
                    else if (!sa_zadnjim_ciframa_kombinacija[i])   //NULA CIFARA
                    {
                        if (_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije) != -1)  //UKLONI CIFRU
                        {
                            sve_cifre_su_dobre = false;
                            for (int j = (pocetak - 1); (j < kraj) && (_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije) != -1); j++)
                                for (int k = 0; (k < duzina_kombinacije) && (_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije) != -1); k++)
                                {
                                    for (int l = 0; l < duzina_kombinacije; l++)
                                    {
                                        nova_kombinacija1[l] = brojevi[i][l];
                                        nova_kombinacija2[l] = brojevi[j][l];
                                    }
                                    nova_kombinacija1[_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije)] = brojevi[j][k];
                                    nova_kombinacija2[k] = brojevi[i][_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije)];
                                    if (sa_zadnjim_ciframa_kombinacija[j])
                                        odrzan_broj_kombinacija_cifre = 1;
                                    else
                                        odrzan_broj_kombinacija_cifre = 0;
                                    if (sa_susednima_kombinacija[j])
                                        odrzan_broj_kombinacija_susedni = 1;
                                    else
                                        odrzan_broj_kombinacija_susedni = 0;
                                    if (petoskupovna_kombinacija[j])
                                        odrzan_broj_kombinacija_skupovi = 5;
                                    else
                                        odrzan_broj_kombinacija_skupovi = 4;
                                    if ((_broj_zadnjih_cifara(brojevi[i], duzina_kombinacije) >= _broj_zadnjih_cifara(nova_kombinacija1, duzina_kombinacije))
                                    && (Math.Abs(_broj_zadnjih_cifara(brojevi[j], duzina_kombinacije) - odrzan_broj_kombinacija_cifre) >= Math.Abs(_broj_zadnjih_cifara(nova_kombinacija2, duzina_kombinacije) - odrzan_broj_kombinacija_cifre))
                                    && (Math.Abs(_broj_susednih(brojevi[j], duzina_kombinacije) - odrzan_broj_kombinacija_susedni) >= Math.Abs(_broj_susednih(nova_kombinacija2, duzina_kombinacije) - odrzan_broj_kombinacija_susedni))
                                    && (Math.Abs(_broj_skupova(brojevi[j], duzina_kombinacije, velicina_skupa) - odrzan_broj_kombinacija_skupovi) >= Math.Abs(_broj_skupova(nova_kombinacija2, duzina_kombinacije, velicina_skupa) - odrzan_broj_kombinacija_skupovi))
                                    && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije)])
                                    && !_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k])
                                    && ((brojevi[i][_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije)] % 2) == (brojevi[j][k] % 2))
                                    && (((brojevi[i][_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije)] <= granica_malih) && (brojevi[j][k] <= granica_malih)) || ((brojevi[i][_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije)] > granica_malih) && (brojevi[j][k] > granica_malih)))
                                    && ((broj_omiljenih_brojeva == -1)
                                    || ((broj_omiljenih_brojeva == 1) && (brojevi[i][_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]))
                                    || ((broj_omiljenih_brojeva == 2) && (brojevi[i][_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]) && (brojevi[i][_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[1]) && (brojevi[j][k] != omiljeni_brojevi[1]))))
                                    {
                                        int temp = brojevi[i][_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije)];
                                        brojevi[i][_indeks_zadnja_cifra_1(brojevi[i], duzina_kombinacije)] = brojevi[j][k];
                                        brojevi[j][k] = temp;
                                    }
                                }
                        }
                    }
                }

                brojac_cifre++;
            }

            int brojac_suma = 0;   //RESAVANJE SUMA
            int broj_dobrih_suma1 = 0;
            int broj_dobrih_suma2 = 0;
            int nova_suma1;
            int nova_suma2;
            bool sve_sume_su_dobre = false;
            while (!sve_sume_su_dobre)
            {
                if (brojac_suma % 3 <= 0)
                {
                    broj_dobrih_suma1 = broj_dobrih_suma2;
                    broj_dobrih_suma2 = 0;
                    for (int i = (pocetak - 1); i < kraj; i++)
                        if ((sume_kombinacija[i] >= suma_min) && (sume_kombinacija[i] >= suma_min))
                            broj_dobrih_suma2++;
                    if (broj_dobrih_suma2 <= broj_dobrih_suma1)
                        break;
                }

                sve_sume_su_dobre = true;
                for (int i = (pocetak - 1); i < kraj; i++)
                {
                    if (sume_kombinacija[i] < suma_min)  //ZA MANJE
                    {
                        sve_sume_su_dobre = false;
                        for (int j = (pocetak - 1); (j < kraj) && (sume_kombinacija[i] < suma_min); j++)
                            for (int k = 0; (k < duzina_kombinacije) && (sume_kombinacija[i] < suma_min); k++)
                            {
                                nasumicni_element = random.Next(0, duzina_kombinacije);
                                nova_suma1 = sume_kombinacija[i] - brojevi[i][nasumicni_element] + brojevi[j][k];
                                nova_suma2 = sume_kombinacija[j] + brojevi[i][nasumicni_element] - brojevi[j][k];
                                if ((nova_suma1 <= suma_max)
                                && (nova_suma2 >= suma_min) && (nova_suma2 <= suma_max)
                                && (nova_suma1 > sume_kombinacija[i])
                                && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][nasumicni_element])
                                && !_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k])
                                && ((brojevi[i][nasumicni_element] % 2) == (brojevi[j][k] % 2))
                                && (((brojevi[i][nasumicni_element] <= granica_malih) && (brojevi[j][k] <= granica_malih)) || ((brojevi[i][nasumicni_element] > granica_malih) && (brojevi[j][k] > granica_malih)))
                                && (((brojevi[i][nasumicni_element] / velicina_skupa) - 1) == ((brojevi[j][k] / velicina_skupa) - 1))
                                && (_broj_susednih_elemenata(brojevi[i], duzina_kombinacije, brojevi[i][nasumicni_element]) == _broj_susednih_elemenata(brojevi[j], duzina_kombinacije, brojevi[j][k]))
                                && (_broj_zadnjih_cifara_elemenata(brojevi[i], duzina_kombinacije, brojevi[i][nasumicni_element]) == _broj_zadnjih_cifara_elemenata(brojevi[j], duzina_kombinacije, brojevi[j][k]))
                                && ((broj_omiljenih_brojeva == -1)
                                || ((broj_omiljenih_brojeva == 1) && (brojevi[i][nasumicni_element] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]))
                                || ((broj_omiljenih_brojeva == 2) && (brojevi[i][nasumicni_element] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]) && (brojevi[i][nasumicni_element] != omiljeni_brojevi[1]) && (brojevi[j][k] != omiljeni_brojevi[1]))))
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
                        for (int j = (pocetak - 1); (j < kraj) && (sume_kombinacija[i] > suma_max); j++)
                            for (int k = 0; (k < duzina_kombinacije) && (sume_kombinacija[i] > suma_max); k++)
                            {
                                nasumicni_element = random.Next(0, duzina_kombinacije);
                                nova_suma1 = sume_kombinacija[i] - brojevi[i][nasumicni_element] + brojevi[j][k];
                                nova_suma2 = sume_kombinacija[j] + brojevi[i][nasumicni_element] - brojevi[j][k];
                                if ((nova_suma1 >= suma_min)
                                && (nova_suma2 >= suma_min) && (nova_suma2 <= suma_max)
                                && (nova_suma1 < sume_kombinacija[i])
                                && !_poseduje_element(brojevi[j], duzina_kombinacije, brojevi[i][nasumicni_element])
                                && !_poseduje_element(brojevi[i], duzina_kombinacije, brojevi[j][k])
                                && ((brojevi[i][nasumicni_element] % 2) == (brojevi[j][k] % 2))
                                && (((brojevi[i][nasumicni_element] <= granica_malih) && (brojevi[j][k] <= granica_malih)) || ((brojevi[i][nasumicni_element] > granica_malih) && (brojevi[j][k] > granica_malih)))
                                && (((brojevi[i][nasumicni_element] / velicina_skupa) - 1) == ((brojevi[j][k] / velicina_skupa) - 1))
                                && (_broj_susednih_elemenata(brojevi[i], duzina_kombinacije, brojevi[i][nasumicni_element]) == _broj_susednih_elemenata(brojevi[j], duzina_kombinacije, brojevi[j][k]))
                                && (_broj_zadnjih_cifara_elemenata(brojevi[i], duzina_kombinacije, brojevi[i][nasumicni_element]) == _broj_zadnjih_cifara_elemenata(brojevi[j], duzina_kombinacije, brojevi[j][k]))
                                && ((broj_omiljenih_brojeva == -1)
                                || ((broj_omiljenih_brojeva == 1) && (brojevi[i][nasumicni_element] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]))
                                || ((broj_omiljenih_brojeva == 2) && (brojevi[i][nasumicni_element] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]) && (brojevi[i][nasumicni_element] != omiljeni_brojevi[1]) && (brojevi[j][k] != omiljeni_brojevi[1]))))
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

                brojac_suma++;
            }

            int brojac_razlika = 0;    //RESAVANJE RAZLIKA
            int broj_dobrih_razlika1 = 0;
            int broj_dobrih_razlika2 = 0;
            int nova_razlika1;
            int nova_razlika2;
            int[] privremena_kombinacija2 = new int[7];
            int izabran_broj;
            bool sve_razlike_su_dobre = false;
            while (!sve_razlike_su_dobre)
            {
                if (brojac_razlika % 3 <= 0)
                {
                    broj_dobrih_razlika1 = broj_dobrih_razlika2;
                    broj_dobrih_razlika2 = 0;
                    for (int i = (pocetak - 1); i < kraj; i++)
                        if ((razlike_kombinacija[i] >= razlika_min) && (razlike_kombinacija[i] >= razlika_min))
                            broj_dobrih_razlika2++;
                    if (broj_dobrih_razlika2 <= broj_dobrih_razlika1)
                        break;
                }

                sve_razlike_su_dobre = true;
                for (int i = (pocetak - 1); i < kraj; i++)
                {
                    if (razlike_kombinacija[i] < razlika_min)  //ZA MANJE
                    {
                        sve_razlike_su_dobre = false;
                        for (int j = (pocetak - 1); (j < kraj) && (razlike_kombinacija[i] < razlika_min); j++)
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
                                    && (nova_suma2 >= suma_min) && (nova_suma2 <= suma_max)
                                    && ((brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] % 2) == (brojevi[j][k] % 2))
                                    && (((brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] <= granica_malih) && (brojevi[j][k] <= granica_malih)) || ((brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] > granica_malih) && (brojevi[j][k] > granica_malih)))
                                    && (((brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] / velicina_skupa) - 1) == ((brojevi[j][k] / velicina_skupa) - 1))
                                    && (_broj_susednih_elemenata(brojevi[i], duzina_kombinacije, brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)]) == _broj_susednih_elemenata(brojevi[j], duzina_kombinacije, brojevi[j][k]))
                                    && (_broj_zadnjih_cifara_elemenata(brojevi[i], duzina_kombinacije, brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)]) == _broj_zadnjih_cifara_elemenata(brojevi[j], duzina_kombinacije, brojevi[j][k]))
                                    && ((broj_omiljenih_brojeva == -1)
                                    || ((broj_omiljenih_brojeva == 1) && (brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]))
                                    || ((broj_omiljenih_brojeva == 2) && (brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]) && (brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[1]) && (brojevi[j][k] != omiljeni_brojevi[1]))))
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
                                    && (nova_suma2 >= suma_min) && (nova_suma2 <= suma_max)
                                    && ((brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] % 2) == (brojevi[j][k] % 2))
                                    && (((brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] <= granica_malih) && (brojevi[j][k] <= granica_malih)) || ((brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] > granica_malih) && (brojevi[j][k] > granica_malih)))
                                    && (((brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] / velicina_skupa) - 1) == ((brojevi[j][k] / velicina_skupa) - 1))
                                    && (_broj_susednih_elemenata(brojevi[i], duzina_kombinacije, brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)]) == _broj_susednih_elemenata(brojevi[j], duzina_kombinacije, brojevi[j][k]))
                                    && (_broj_zadnjih_cifara_elemenata(brojevi[i], duzina_kombinacije, brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)]) == _broj_zadnjih_cifara_elemenata(brojevi[j], duzina_kombinacije, brojevi[j][k])) && ((broj_omiljenih_brojeva == -1)
                                    || ((broj_omiljenih_brojeva == 1) && (brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]))
                                    || ((broj_omiljenih_brojeva == 2) && (brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]) && (brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[1]) && (brojevi[j][k] != omiljeni_brojevi[1]))))
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
                        for (int j = (pocetak - 1); (j < kraj) && (razlike_kombinacija[i] > razlika_max); j++)
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
                                    && (nova_suma2 >= suma_min) && (nova_suma2 <= suma_max)
                                    && ((brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] % 2) == (brojevi[j][k] % 2))
                                    && (((brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] <= granica_malih) && (brojevi[j][k] <= granica_malih)) || ((brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] > granica_malih) && (brojevi[j][k] > granica_malih)))
                                    && (((brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] / velicina_skupa) - 1) == ((brojevi[j][k] / velicina_skupa) - 1))
                                    && (_broj_susednih_elemenata(brojevi[i], duzina_kombinacije, brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)]) == _broj_susednih_elemenata(brojevi[j], duzina_kombinacije, brojevi[j][k]))
                                    && (_broj_zadnjih_cifara_elemenata(brojevi[i], duzina_kombinacije, brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)]) == _broj_zadnjih_cifara_elemenata(brojevi[j], duzina_kombinacije, brojevi[j][k]))
                                    && ((broj_omiljenih_brojeva == -1)
                                    || ((broj_omiljenih_brojeva == 1) && (brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]))
                                    || ((broj_omiljenih_brojeva == 2) && (brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]) && (brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[1]) && (brojevi[j][k] != omiljeni_brojevi[1]))))
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
                                    && (nova_suma2 >= suma_min) && (nova_suma2 <= suma_max)
                                    && ((brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] % 2) == (brojevi[j][k] % 2))
                                    && (((brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] <= granica_malih) && (brojevi[j][k] <= granica_malih)) || ((brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] > granica_malih) && (brojevi[j][k] > granica_malih)))
                                    && (((brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] / velicina_skupa) - 1) == ((brojevi[j][k] / velicina_skupa) - 1))
                                    && (_broj_susednih_elemenata(brojevi[i], duzina_kombinacije, brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)]) == _broj_susednih_elemenata(brojevi[j], duzina_kombinacije, brojevi[j][k]))
                                    && (_broj_zadnjih_cifara_elemenata(brojevi[i], duzina_kombinacije, brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)]) == _broj_zadnjih_cifara_elemenata(brojevi[j], duzina_kombinacije, brojevi[j][k]))
                                    && ((broj_omiljenih_brojeva == -1)
                                    || ((broj_omiljenih_brojeva == 1) && (brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]))
                                    || ((broj_omiljenih_brojeva == 2) && (brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[0]) && (brojevi[j][k] != omiljeni_brojevi[0]) && (brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] != omiljeni_brojevi[1]) && (brojevi[j][k] != omiljeni_brojevi[1]))))
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

                brojac_razlika++;
            }
        }

        public static List<List<int>> _neke_kombinacije(int broj_loptica, int duzina_kombinacije, int broj_kombinacija, List<int> zabranjeni_brojevi_lista, int broj_zabranjenih_brojeva, List<int> omiljeni_brojevi_lista, int broj_omiljenih_brojeva, int procenat_pojavljivana_omiljenih_brojeva)
        {
            if (broj_omiljenih_brojeva == 0)
                broj_omiljenih_brojeva = -1;
            if (broj_zabranjenih_brojeva == 0)
                broj_zabranjenih_brojeva = -1;

            int[] omiljeni_brojevi = new int[2];
            for (int i = 0; i < broj_omiljenih_brojeva; i++)
                omiljeni_brojevi[i] = omiljeni_brojevi_lista[i];
            int[] zabranjeni_brojevi = new int[2];
            for (int i = 0; i < broj_zabranjenih_brojeva; i++)
                zabranjeni_brojevi[i] = zabranjeni_brojevi_lista[i];

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
            {
                upotrebljeni_brojevi[i] = false;
                if (broj_zabranjenih_brojeva != -1)
                    for (int j = 0; j < broj_zabranjenih_brojeva; j++)
                        if ((i + 1) == zabranjeni_brojevi[j])
                            upotrebljeni_brojevi[i] = true;
                if (broj_omiljenih_brojeva != -1)
                    for (int j = 0; j < broj_omiljenih_brojeva; j++)
                        if ((i + 1) == omiljeni_brojevi[j])
                            upotrebljeni_brojevi[i] = true;
            }

            int ukupan_broj_parnih = 0; //STVARANJE BROJEVA
            int ukupan_broj_neparnih = 0;
            int ukupan_broj_malih = 0;
            int ukupan_broj_velikih = 0;
            int broj_brojeva = broj_kombinacija * duzina_kombinacije;
            int broj_brojeva_u_ciklusu = broj_loptica;
            if (broj_zabranjenih_brojeva != -1)
                broj_brojeva_u_ciklusu -= broj_zabranjenih_brojeva;
            if (broj_zabranjenih_brojeva != -1)
                broj_brojeva_u_ciklusu -= broj_omiljenih_brojeva;
            int[][] brojevi = new int[15400000][];
            int trenutni_broj = 1;
            int indeks_omiljenog_broja = 0;
            bool moguc_broj;
            int indeks_deljenja = 0;
            int pocetak_kruga;
            if (procenat_pojavljivana_omiljenih_brojeva == 100)
                indeks_deljenja = 1;
            else if (procenat_pojavljivana_omiljenih_brojeva == 50)
                indeks_deljenja = 2;
            if (broj_omiljenih_brojeva == -1)
                pocetak_kruga = 0;
            else
                pocetak_kruga = (broj_omiljenih_brojeva * broj_kombinacija / indeks_deljenja);
            for (int i = 0; i < ((broj_brojeva - pocetak_kruga) / broj_brojeva_u_ciklusu * broj_brojeva_u_ciklusu + pocetak_kruga); i++)
            {
                if (((broj_omiljenih_brojeva == 1) && (i % duzina_kombinacije == 0) && (procenat_pojavljivana_omiljenih_brojeva == 100))
                || ((broj_omiljenih_brojeva == 2) && ((i % duzina_kombinacije == 0) || (i % duzina_kombinacije == 1)) && (procenat_pojavljivana_omiljenih_brojeva == 100))
                || ((broj_omiljenih_brojeva == 1) && ((i % duzina_kombinacije == 0) && (i < (broj_brojeva / 2))) && (procenat_pojavljivana_omiljenih_brojeva == 50))
                || ((broj_omiljenih_brojeva == 2) && (((i % duzina_kombinacije == 0) || (i % duzina_kombinacije == 1)) && (i < (broj_brojeva / 2)) && (procenat_pojavljivana_omiljenih_brojeva == 50))))
                {
                    if (indeks_omiljenog_broja == broj_omiljenih_brojeva)
                        indeks_omiljenog_broja = 0;

                    if ((omiljeni_brojevi[indeks_omiljenog_broja] % 2) == 0)
                        ukupan_broj_parnih++;
                    else if ((omiljeni_brojevi[indeks_omiljenog_broja] % 2) != 0)
                        ukupan_broj_neparnih++;
                    if (omiljeni_brojevi[indeks_omiljenog_broja] <= granica_malih)
                        ukupan_broj_malih++;
                    else if (omiljeni_brojevi[indeks_omiljenog_broja] > granica_malih)
                        ukupan_broj_velikih++;
                    if (i % duzina_kombinacije == 0)
                        brojevi[i / duzina_kombinacije] = new int[7];
                    brojevi[i / duzina_kombinacije][i % duzina_kombinacije] = omiljeni_brojevi[indeks_omiljenog_broja];
                    indeks_omiljenog_broja++;
                }
                else
                {
                    if (trenutni_broj > broj_loptica)
                        trenutni_broj = 1;

                    moguc_broj = false;
                    while (!moguc_broj)
                    {
                        moguc_broj = true;
                        if (broj_zabranjenih_brojeva != -1)
                        {
                            for (int j = 0; j < broj_zabranjenih_brojeva; j++)
                                if (trenutni_broj == zabranjeni_brojevi[j])
                                {
                                    trenutni_broj++;
                                    moguc_broj = false;
                                }
                        }
                        if (broj_omiljenih_brojeva != -1)
                        {
                            for (int j = 0; j < broj_omiljenih_brojeva; j++)
                                if (trenutni_broj == omiljeni_brojevi[j])
                                {
                                    trenutni_broj++;
                                    moguc_broj = false;
                                }
                        }
                    }

                    if ((trenutni_broj % 2) == 0)
                        ukupan_broj_parnih++;
                    else if ((trenutni_broj % 2) != 0)
                        ukupan_broj_neparnih++;
                    if (trenutni_broj <= granica_malih)
                        ukupan_broj_malih++;
                    else if (trenutni_broj > granica_malih)
                        ukupan_broj_velikih++;
                    if (i % duzina_kombinacije == 0)
                        brojevi[i / duzina_kombinacije] = new int[7];
                    brojevi[i / duzina_kombinacije][i % duzina_kombinacije] = trenutni_broj;

                    trenutni_broj++;
                }
            }
            trenutni_broj = 1;
            int brojac = 0;
            bool broj_nadjen = false;
            for (int i = ((broj_brojeva - pocetak_kruga) / broj_brojeva_u_ciklusu * broj_brojeva_u_ciklusu + pocetak_kruga); i < broj_brojeva; i++)
            {
                if (((broj_omiljenih_brojeva == 1) && (i % duzina_kombinacije == 0) && (procenat_pojavljivana_omiljenih_brojeva == 100))
                || ((broj_omiljenih_brojeva == 2) && ((i % duzina_kombinacije == 0) || (i % duzina_kombinacije == 1)) && (procenat_pojavljivana_omiljenih_brojeva == 100))
                || ((broj_omiljenih_brojeva == 1) && ((i % duzina_kombinacije == 0) && (i < (broj_brojeva / 2))) && (procenat_pojavljivana_omiljenih_brojeva == 50))
                || ((broj_omiljenih_brojeva == 2) && (((i % duzina_kombinacije == 0) || (i % duzina_kombinacije == 1)) && (i < (broj_brojeva / 2)) && (procenat_pojavljivana_omiljenih_brojeva == 50))))
                {
                    if (indeks_omiljenog_broja == broj_omiljenih_brojeva)
                        indeks_omiljenog_broja = 0;

                    if ((omiljeni_brojevi[indeks_omiljenog_broja] % 2) == 0)
                        ukupan_broj_parnih++;
                    else if ((omiljeni_brojevi[indeks_omiljenog_broja] % 2) != 0)
                        ukupan_broj_neparnih++;
                    if (omiljeni_brojevi[indeks_omiljenog_broja] <= granica_malih)
                        ukupan_broj_malih++;
                    else if (omiljeni_brojevi[indeks_omiljenog_broja] > granica_malih)
                        ukupan_broj_velikih++;
                    if (i % duzina_kombinacije == 0)
                        brojevi[i / duzina_kombinacije] = new int[7];
                    brojevi[i / duzina_kombinacije][i % duzina_kombinacije] = omiljeni_brojevi[indeks_omiljenog_broja];
                    indeks_omiljenog_broja++;
                }
                else
                {
                    brojac = 0;
                    broj_nadjen = false;
                    while (broj_nadjen == false)
                    {
                        if (brojac == 100)
                        {
                            for (int j = 0; j < broj_loptica; j++)
                            {
                                upotrebljeni_brojevi[j] = false;
                                if (broj_zabranjenih_brojeva != -1)
                                    for (int k = 0; k < broj_zabranjenih_brojeva; k++)
                                        if ((j + 1) == zabranjeni_brojevi[k])
                                            upotrebljeni_brojevi[j] = true;
                                if (broj_omiljenih_brojeva != -1)
                                    for (int k = 0; k < broj_omiljenih_brojeva; k++)
                                        if ((j + 1) == omiljeni_brojevi[k])
                                            upotrebljeni_brojevi[j] = true;
                            }
                            brojac = 0;
                            trenutni_broj = 1;
                        }

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
                            if (trenutni_broj > broj_loptica)
                                trenutni_broj = 1;
                            broj_nadjen = true;
                            upotrebljeni_brojevi[trenutni_broj - 1] = true;
                        }
                        if (trenutni_broj > broj_loptica)
                            trenutni_broj = 1;

                        brojac++;
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
            }

            Random random = new Random();  //MESANJE BROJEVA
            if (broj_omiljenih_brojeva == -1)
            {
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
            }
            else if (broj_omiljenih_brojeva == 1)
            {
                for (int i = broj_brojeva - 1; i > 0; i--)
                {
                    int j = random.Next(0, i + 1);

                    int red_i = i / duzina_kombinacije;
                    int kolona_i = i % duzina_kombinacije;
                    int red_j = j / duzina_kombinacije;
                    int kolona_j = j % duzina_kombinacije;
                    if (kolona_i <= 0)
                        kolona_i = 1;
                    if (kolona_j <= 0)
                        kolona_j = 1;
                    int temp = brojevi[red_i][kolona_i];
                    brojevi[red_i][kolona_i] = brojevi[red_j][kolona_j];
                    brojevi[red_j][kolona_j] = temp;
                }
            }
            else if (broj_omiljenih_brojeva == 2)
            {
                for (int i = broj_brojeva - 1; i > 0; i--)
                {
                    int j = random.Next(0, i + 1);

                    int red_i = i / duzina_kombinacije;
                    int kolona_i = i % duzina_kombinacije;
                    int red_j = j / duzina_kombinacije;
                    int kolona_j = j % duzina_kombinacije;
                    if (kolona_i <= 1)
                        kolona_i = 2;
                    if (kolona_j <= 1)
                        kolona_j = 2;
                    int temp = brojevi[red_i][kolona_i];
                    brojevi[red_i][kolona_i] = brojevi[red_j][kolona_j];
                    brojevi[red_j][kolona_j] = temp;
                }
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

            bool[] tabela_skupovi = new bool[0];    //TABELE SKUPOVI, SUSEDNI I ZADNJE CIFRE
            bool[] tabela_susedni = new bool[0];
            bool[] tabela_zadnje_cifre = new bool[0];
            if (duzina_kombinacije == 6)
            {
                tabela_skupovi = new bool[] { false, true, false, true, false, true, false, true, false, true };
                tabela_susedni = new bool[] { false, true, false, true, false, false, true, false, true, false };
                tabela_zadnje_cifre = new bool[] { false, true, false, true, false, false, true, false, true, false };
            }
            else if (duzina_kombinacije == 7)
            {
                tabela_skupovi = new bool[] { false, true, false, true, false, true, false, true, false, true };
                tabela_susedni = new bool[] { false, true, false, true, false, false, true, false, true, false };
                tabela_zadnje_cifre = new bool[] { false, true, false, true, true, false, true, false, true, true };
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
            bool[] petoskupovna_kombinacija = new bool[15400000];
            bool[] sa_susednima_kombinacija = new bool[15400000];
            bool[] sa_zadnjim_ciframa_kombinacija = new bool[15400000];
            int biracka_pozicija_skupovi = random.Next(0, 10);
            int biracka_pozicija_susedni = random.Next(0, 10);
            int biracka_pozicija_zadnje_cifre = random.Next(0, 10);
            petoskupovna_kombinacija[0] = tabela_skupovi[biracka_pozicija_skupovi];
            biracka_pozicija_skupovi++;
            if (biracka_pozicija_skupovi == 10)
                biracka_pozicija_skupovi = 0;
            sa_susednima_kombinacija[0] = tabela_susedni[biracka_pozicija_susedni];
            biracka_pozicija_susedni++;
            if (biracka_pozicija_susedni == 10)
                biracka_pozicija_susedni = 0;
            sa_zadnjim_ciframa_kombinacija[0] = tabela_zadnje_cifre[biracka_pozicija_zadnje_cifre];
            biracka_pozicija_zadnje_cifre++;
            if (biracka_pozicija_zadnje_cifre == 10)
                biracka_pozicija_zadnje_cifre = 0;
            for (int i = 1; i < broj_kombinacija; i++)
            {
                if ((broj_parnih_brojeva_kombinacija[i] != broj_parnih_brojeva_kombinacija[i - 1]) || (broj_malih_brojeva_kombinacija[i] != broj_malih_brojeva_kombinacija[i - 1]))
                {
                    biracka_pozicija_skupovi = random.Next(0, 10);
                    biracka_pozicija_susedni = random.Next(0, 10);
                    biracka_pozicija_zadnje_cifre = random.Next(0, 10);
                }
                petoskupovna_kombinacija[i] = tabela_skupovi[biracka_pozicija_skupovi];
                biracka_pozicija_skupovi++;
                if (biracka_pozicija_skupovi == 10)
                    biracka_pozicija_skupovi = 0;
                sa_susednima_kombinacija[i] = tabela_susedni[biracka_pozicija_susedni];
                biracka_pozicija_susedni++;
                if (biracka_pozicija_susedni == 10)
                    biracka_pozicija_susedni = 0;
                sa_zadnjim_ciframa_kombinacija[i] = tabela_zadnje_cifre[biracka_pozicija_zadnje_cifre];
                biracka_pozicija_zadnje_cifre++;
                if (biracka_pozicija_zadnje_cifre == 10)
                    biracka_pozicija_zadnje_cifre = 0;
            }

            int[] sume_kombinacija = new int[15400000];   //STVARANJE SUMA
            int suma;
            for (int i = 0; i < broj_kombinacija; i++)
            {
                suma = 0;
                for (int j = 0; j < duzina_kombinacije; j++)
                    suma += brojevi[i][j];
                sume_kombinacija[i] = suma;
            }

            int[] razlike_kombinacija = new int[15400000];  //STVARANJE RAZLIKA
            for (int i = 0; i < broj_kombinacija; i++)
                razlike_kombinacija[i] = brojevi[i][_indeks_max(brojevi[i], duzina_kombinacije)] - brojevi[i][_indeks_min(brojevi[i], duzina_kombinacije)];


            int pocetna_kombinacija = 1;    //ODRADJIVANJE ALGORITMA PO STOTINAMA
            int krajnja_kombinacija = 100;
            if (krajnja_kombinacija > broj_kombinacija)
                krajnja_kombinacija = broj_kombinacija;
            while (pocetna_kombinacija <= broj_kombinacija)
            {
                _resi_100_kombinacija(brojevi, sume_kombinacija, razlike_kombinacija, pocetna_kombinacija, krajnja_kombinacija, duzina_kombinacije, broj_loptica, granica_malih, suma_min, suma_max, razlika_min, razlika_max, broj_parnih_brojeva_kombinacija, broj_malih_brojeva_kombinacija, velicina_skupa, petoskupovna_kombinacija, sa_susednima_kombinacija, sa_zadnjim_ciframa_kombinacija, omiljeni_brojevi, broj_omiljenih_brojeva);
                pocetna_kombinacija += 100;
                krajnja_kombinacija += 100;
                if (krajnja_kombinacija > broj_kombinacija)
                    krajnja_kombinacija = broj_kombinacija;
            }

            int[][] dobri_brojevi = new int[15400000][];    //ODRADJIVANJE ALGORITMA CISCENJE
            int broj_dobrih = 101;
            int[][] losi_brojevi = new int[15400000][];
            int broj_losih = 101;
            int[] sume_kombinacija_dobre = new int[15400000];
            int[] sume_kombinacija_lose = new int[15400000];
            int[] razlike_kombinacija_dobre = new int[15400000];
            int[] razlike_kombinacija_lose = new int[15400000];
            int[] broj_parnih_brojeva_kombinacija_dobre = new int[15400000];
            int[] broj_parnih_brojeva_kombinacija_lose = new int[15400000];
            int[] broj_malih_brojeva_kombinacija_dobre = new int[15400000];
            int[] broj_malih_brojeva_kombinacija_lose = new int[15400000];
            bool[] petoskupovna_kombinacija_dobre = new bool[15400000];
            bool[] petoskupovna_kombinacija_lose = new bool[15400000];
            bool[] sa_susednima_kombinacija_dobre = new bool[15400000];
            bool[] sa_susednima_kombinacija_lose = new bool[15400000];
            bool[] sa_zadnjim_ciframa_kombinacija_dobre = new bool[15400000];
            bool[] sa_zadnjim_ciframa_kombinacija_lose = new bool[15400000];
            brojac = 0;
            while (broj_losih > 100)
            {
                if (brojac > 2)
                    break;

                broj_dobrih = 0;    //RAZLAGANJE
                broj_losih = 0;
                for (int i = 0; i < broj_kombinacija; i++)
                {
                    if ((_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica) != -1)
                    || (broj_parnih_brojeva_kombinacija[i] != _broj_parnih(brojevi[i], duzina_kombinacije))
                    || (broj_malih_brojeva_kombinacija[i] != _broj_malih(brojevi[i], duzina_kombinacije, granica_malih))
                    || ((sume_kombinacija[i] < suma_min) && (sume_kombinacija[i] > suma_max))
                    || ((razlike_kombinacija[i] < razlika_min) && (razlike_kombinacija[i] > razlika_max)))
                    {
                        losi_brojevi[broj_losih] = new int[7];
                        for (int j = 0; j < duzina_kombinacije; j++)
                            losi_brojevi[broj_losih][j] = brojevi[i][j];
                        sume_kombinacija_lose[broj_losih] = sume_kombinacija[i];
                        razlike_kombinacija_lose[broj_losih] = razlike_kombinacija[i];
                        broj_parnih_brojeva_kombinacija_lose[broj_losih] = broj_parnih_brojeva_kombinacija[i];
                        broj_malih_brojeva_kombinacija_lose[broj_losih] = broj_malih_brojeva_kombinacija[i];
                        petoskupovna_kombinacija_lose[broj_losih] = petoskupovna_kombinacija[i];
                        sa_susednima_kombinacija_lose[broj_losih] = sa_susednima_kombinacija[i];
                        sa_zadnjim_ciframa_kombinacija_lose[broj_losih] = sa_zadnjim_ciframa_kombinacija[i];

                        broj_losih++;
                    }
                    else
                    {
                        dobri_brojevi[broj_dobrih] = new int[7];
                        for (int j = 0; j < duzina_kombinacije; j++)
                            dobri_brojevi[broj_dobrih][j] = brojevi[i][j];
                        sume_kombinacija_dobre[broj_dobrih] = sume_kombinacija[i];
                        razlike_kombinacija_dobre[broj_dobrih] = razlike_kombinacija[i];
                        broj_parnih_brojeva_kombinacija_dobre[broj_dobrih] = broj_parnih_brojeva_kombinacija[i];
                        broj_malih_brojeva_kombinacija_dobre[broj_dobrih] = broj_malih_brojeva_kombinacija[i];
                        petoskupovna_kombinacija_dobre[broj_dobrih] = petoskupovna_kombinacija[i];
                        sa_susednima_kombinacija_dobre[broj_dobrih] = sa_susednima_kombinacija[i];
                        sa_zadnjim_ciframa_kombinacija_dobre[broj_dobrih] = sa_zadnjim_ciframa_kombinacija[i];

                        broj_dobrih++;
                    }
                }

                pocetna_kombinacija = 1;    //RESAVANJE
                krajnja_kombinacija = 100;
                if (krajnja_kombinacija > broj_losih)
                    krajnja_kombinacija = broj_losih;
                while (pocetna_kombinacija <= broj_losih)
                {
                    _resi_100_kombinacija(losi_brojevi, sume_kombinacija_lose, razlike_kombinacija_lose, pocetna_kombinacija, krajnja_kombinacija, duzina_kombinacije, broj_loptica, granica_malih, suma_min, suma_max, razlika_min, razlika_max, broj_parnih_brojeva_kombinacija_lose, broj_malih_brojeva_kombinacija_lose, velicina_skupa, petoskupovna_kombinacija_lose, sa_susednima_kombinacija_lose, sa_zadnjim_ciframa_kombinacija_lose, omiljeni_brojevi, broj_omiljenih_brojeva);
                    pocetna_kombinacija += 100;
                    krajnja_kombinacija += 100;
                    if (krajnja_kombinacija > broj_losih)
                        krajnja_kombinacija = broj_losih;
                }

                for (int i = 0; i < broj_dobrih; i++)  //SASTAVLJANJE
                {
                    for (int j = 0; j < duzina_kombinacije; j++)
                        brojevi[i][j] = dobri_brojevi[i][j];
                    sume_kombinacija[i] = sume_kombinacija_dobre[i];
                    razlike_kombinacija[i] = razlike_kombinacija_dobre[i];
                    broj_parnih_brojeva_kombinacija[i] = broj_parnih_brojeva_kombinacija_dobre[i];
                    broj_malih_brojeva_kombinacija[i] = broj_malih_brojeva_kombinacija_dobre[i];
                    petoskupovna_kombinacija[i] = petoskupovna_kombinacija_dobre[i];
                    sa_susednima_kombinacija[i] = sa_susednima_kombinacija_dobre[i];
                    sa_zadnjim_ciframa_kombinacija[i] = sa_zadnjim_ciframa_kombinacija_dobre[i];
                }
                for (int i = broj_dobrih; i < broj_kombinacija; i++)
                {
                    for (int j = 0; j < duzina_kombinacije; j++)
                        brojevi[i][j] = losi_brojevi[i - broj_dobrih][j];
                    sume_kombinacija[i] = sume_kombinacija_lose[i - broj_dobrih];
                    razlike_kombinacija[i] = razlike_kombinacija_lose[i - broj_dobrih];
                    broj_parnih_brojeva_kombinacija[i] = broj_parnih_brojeva_kombinacija_lose[i - broj_dobrih];
                    broj_malih_brojeva_kombinacija[i] = broj_malih_brojeva_kombinacija_lose[i - broj_dobrih];
                    petoskupovna_kombinacija[i] = petoskupovna_kombinacija_lose[i - broj_dobrih];
                    sa_susednima_kombinacija[i] = sa_susednima_kombinacija_lose[i - broj_dobrih];
                    sa_zadnjim_ciframa_kombinacija[i] = sa_zadnjim_ciframa_kombinacija_lose[i - broj_dobrih];
                }

                brojac++;
            }

            if (broj_omiljenih_brojeva >= 1)    //MESANJE KOMBINACIJA POSEBNO
            {
                for (int i = broj_brojeva - 1; i > 0; i--)
                {
                    int j = random.Next(0, i + 1);

                    int red_i = i / duzina_kombinacije;
                    int kolona_i = i % duzina_kombinacije;
                    int red_j = j / duzina_kombinacije;
                    int kolona_j = j % duzina_kombinacije;

                    red_j = red_i;

                    int temp = brojevi[red_i][kolona_i];
                    brojevi[red_i][kolona_i] = brojevi[red_j][kolona_j];
                    brojevi[red_j][kolona_j] = temp;
                }
                for (int i = broj_kombinacija; i > 0; i--)
                {
                    int j = random.Next(0, i + 1);

                    for (int k = 0; k < duzina_kombinacije; k++)
                    {
                        int temp = brojevi[i][k];
                        brojevi[i][k] = brojevi[j][k];
                        brojevi[j][k] = temp;
                    }
                }
            }

            /*for (int i = 0; i < broj_kombinacija; i++)  //ISPIS
            {
                Console.Write((i + 1) + ": ");    //REDNI BROJEVI

                for (int j = 0; j < duzina_kombinacije; j++)    //KOMBINACIJE
                {
                    //if (brojevi[i][j] % 2 == 0)
                    //Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(brojevi[i][j] + "\t");
                    //Console.ResetColor();
                }
                Console.Write("\n");

                /*if (_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica) != -1)   //DA LI IMA PONAVLJANJA BROJEVA
                    Console.Write("-\t");
                else
                    Console.Write("+\t");

                if ((sume_kombinacija[i] > suma_max) || (sume_kombinacija[i] < suma_min))   //DA LI JE SUMA U OPSEGU
                    Console.Write("-\t");
                else
                    Console.Write("+\t");

                if ((razlike_kombinacija[i] > razlika_max) || (razlike_kombinacija[i] < razlika_min))   //RAZLIKA NAJMANJEG I NAJVECEG
                    Console.Write("-\t");
                else
                    Console.Write("+\t");

                //Console.Write(broj_parnih_brojeva_kombinacija[i] + "\t");   //BROJ PARNIH BROJEVA U KOMBINACIJAMA

                if (broj_parnih_brojeva_kombinacija[i] == _broj_parnih(brojevi[i], duzina_kombinacije))
                    Console.Write("+\t");
                else
                    Console.Write("-\t");

                //Console.Write(broj_malih_brojeva_kombinacija[i] + "\t");   //BROJ MALIH BROJEVA U KOMBINACIJAMA

                if (broj_malih_brojeva_kombinacija[i] == _broj_malih(brojevi[i], duzina_kombinacije, granica_malih))
                    Console.Write("+\t");
                else
                    Console.Write("-\t");

                //Console.Write(petoskupovna_kombinacija[i] + "\t");  //POMOCNE OSOBINE KOMBINACIJA

                //Console.Write(_broj_skupova(brojevi[i], duzina_kombinacije, velicina_skupa) + "\t");

                if ((petoskupovna_kombinacija[i] && (_broj_skupova(brojevi[i], duzina_kombinacije, velicina_skupa) == 5)) || (!petoskupovna_kombinacija[i] && (_broj_skupova(brojevi[i], duzina_kombinacije, velicina_skupa) == 4)))
                    Console.Write("+\t");
                else
                    Console.Write("-\t");

                //Console.Write(sa_susednima_kombinacija[i] + "\t");

                //Console.Write(_broj_susednih(brojevi[i], duzina_kombinacije) + "\t");

                if ((sa_susednima_kombinacija[i] && (_broj_susednih(brojevi[i], duzina_kombinacije) == 1)) || (!sa_susednima_kombinacija[i] && (_broj_susednih(brojevi[i], duzina_kombinacije) == 0)))
                    Console.Write("+\t");
                else
                    Console.Write("-\t");

                //Console.Write(sa_zadnjim_ciframa_kombinacija[i] + "\t");

                //Console.Write(_broj_zadnjih_cifara(brojevi[i], duzina_kombinacije) + "\t");

                if ((sa_zadnjim_ciframa_kombinacija[i] && (_broj_zadnjih_cifara(brojevi[i], duzina_kombinacije) == 1)) || (!sa_zadnjim_ciframa_kombinacija[i] && (_broj_zadnjih_cifara(brojevi[i], duzina_kombinacije) == 0)))
                    Console.Write("+\n");
                else
                    Console.Write("-\n");
            }*/

            List<List<int>> neke_kombinacije = new List<List<int>>();   //2D ARRAY -----> LIST
            for (int i = 0; i < broj_kombinacija; i++)
            {
                neke_kombinacije.Add(new List<int>());
                for (int j = 0; j < duzina_kombinacije; j++)
                    neke_kombinacije[i].Add(brojevi[i][j]);
            }
            
            return neke_kombinacije;
        }

        public static List<List<int>> _sve_kombinacije(int broj_loptica, int duzina_kombinacije, List<int> zabranjeni_brojevi_lista, int broj_zabranjenih_brojeva)
        {
            if (broj_zabranjenih_brojeva == 0)
                broj_zabranjenih_brojeva = -1;

            int[] zabranjeni_brojevi = new int[2];
            for (int i = 0; i < broj_zabranjenih_brojeva; i++)
                zabranjeni_brojevi[i] = zabranjeni_brojevi_lista[i];

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
                && (_indeks_susedni_2_plus_para(brojevi[i], duzina_kombinacije) == -1)
                && (_indeks_skupovi_visak_4(brojevi[i], duzina_kombinacije, velicina_skupa) == -1)
                && ((broj_zabranjenih_brojeva == -1)
                    || ((broj_zabranjenih_brojeva == 1) && !_poseduje_element(brojevi[i], duzina_kombinacije, zabranjeni_brojevi[0]))
                    || ((broj_zabranjenih_brojeva == 2) && !_poseduje_element(brojevi[i], duzina_kombinacije, zabranjeni_brojevi[0]) && !_poseduje_element(brojevi[i], duzina_kombinacije, zabranjeni_brojevi[1]))
                    || ((broj_zabranjenih_brojeva == 3) && !_poseduje_element(brojevi[i], duzina_kombinacije, zabranjeni_brojevi[0]) && !_poseduje_element(brojevi[i], duzina_kombinacije, zabranjeni_brojevi[1]) && !_poseduje_element(brojevi[i], duzina_kombinacije, zabranjeni_brojevi[2]))
                    || ((broj_zabranjenih_brojeva == 4) && !_poseduje_element(brojevi[i], duzina_kombinacije, zabranjeni_brojevi[0]) && !_poseduje_element(brojevi[i], duzina_kombinacije, zabranjeni_brojevi[1]) && !_poseduje_element(brojevi[i], duzina_kombinacije, zabranjeni_brojevi[2]) && !_poseduje_element(brojevi[i], duzina_kombinacije, zabranjeni_brojevi[3]))
                    || ((broj_zabranjenih_brojeva == 5) && !_poseduje_element(brojevi[i], duzina_kombinacije, zabranjeni_brojevi[0]) && !_poseduje_element(brojevi[i], duzina_kombinacije, zabranjeni_brojevi[1]) && !_poseduje_element(brojevi[i], duzina_kombinacije, zabranjeni_brojevi[2]) && !_poseduje_element(brojevi[i], duzina_kombinacije, zabranjeni_brojevi[3]) && !_poseduje_element(brojevi[i], duzina_kombinacije, zabranjeni_brojevi[4]))))
                {
                    for (int j = 0; j < duzina_kombinacije; j++)
                        brojevi[red2][j] = brojevi[i][j];
                    red2++;
                }

            /*for (int i = 0; i < red2; i++)  //ISPIS
            {
                Console.Write((i + 1) + ":\t");    //REDNI BROJEVI

                for (int j = 0; j < duzina_kombinacije; j++)    //KOMBINACIJE
                    Console.Write(brojevi[i][j] + "\t");
                Console.Write("\n");

                /*if (_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica) == -1)   //DA LI IMA PONAVLJANJA BROJEVA
                    Console.Write("-1\t");
                else
                    Console.Write(brojevi[i][_indeks_dupli(brojevi[i], duzina_kombinacije, broj_loptica)] + "\t");

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
                    Console.Write(_indeks_zadnja_cifra(brojevi[i], duzina_kombinacije, zadnje_cifre[i]) + "\n");*/

                /*if (_indeks_susedni(brojevi[i], duzina_kombinacije) == -1)   //ZADNJE CIFRE
                    Console.Write("-1\n");
                else
                    Console.Write(_indeks_susedni(brojevi[i], duzina_kombinacije) + "\n");

                if (_indeks_skupovi_visak(brojevi[i], duzina_kombinacije, velicina_skupa) == -1)   //ZADNJE CIFRE
                    Console.Write("-1\n");
                else
                    Console.Write(_indeks_susedni(brojevi[i], duzina_kombinacije) + "\n");
            }*/

             List<List<int>> sve_kombinacije = new List<List<int>>();   //2D ARRAY -----> LIST
                for (int i = 0; i < red2; i++)
                {
                    sve_kombinacije.Add(new List<int>());
                    for (int j = 0; j < duzina_kombinacije; j++)
                        sve_kombinacije[i].Add(brojevi[i][j]);
                }

                return sve_kombinacije;
        }

        /*static void Main(string[] args)
        {
            string biranje_moda = Console.ReadLine();  //BIRANJE MODA

            if (biranje_moda == "neke")
            {
                int broj_loptica = int.Parse(Console.ReadLine());   //UNOS
                int duzina_kombinacije = int.Parse(Console.ReadLine());
                int broj_kombinacija = int.Parse(Console.ReadLine());
                int[] zabranjeni_brojevi = new int[] { 1, 5, 7, 8, 11 };
                int broj_zabranjenih_brojeva = 5;
                int[] omiljeni_brojevi = new int[] { 2, 19 };
                int broj_omiljenih_brojeva = 2;
                int procenat_pojavljivanja_omiljenih_brojeva = 100;

                _neke_kombinacije(broj_loptica, duzina_kombinacije, broj_kombinacija, zabranjeni_brojevi, broj_zabranjenih_brojeva, omiljeni_brojevi, broj_omiljenih_brojeva, procenat_pojavljivanja_omiljenih_brojeva);
            }
            else if (biranje_moda == "sve")
            {
                int broj_loptica = int.Parse(Console.ReadLine());   //UNOS
                int duzina_kombinacije = int.Parse(Console.ReadLine());
                int[] zabranjeni_brojevi = new int[] { 1, 5, 7, 8, 11 };
                int broj_zabranjenih_brojeva = 5;

                _sve_kombinacije(broj_loptica, duzina_kombinacije, zabranjeni_brojevi, broj_zabranjenih_brojeva);
            }
        }*/
    }
}