using System;
using System.Collections.Generic;
using System.Linq;

abstract class PhuongTien
{
    private string _maPT;
    private string _tenHang;
    private int _namSanXuat;
    private decimal _giaGoc;

    public string MaPT
    {
        get { return _maPT; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                _maPT = "PT000";
            else
                _maPT = value;
        }
    }

    public string TenHang
    {
        get { return _tenHang; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Ten hang khong duoc de trong!");
            _tenHang = value;
        }
    }

    public int NamSanXuat
    {
        get { return _namSanXuat; }
        set
        {
            if (value < 1900 || value > DateTime.Now.Year)
                throw new ArgumentException("Nam san xuat khong hop le!");
            _namSanXuat = value;
        }
    }

    public decimal GiaGoc
    {
        get { return _giaGoc; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Gia goc phai lon hon 0!");
            _giaGoc = value;
        }
    }

    public PhuongTien(string ma, string ten, int nam, decimal gia)
    {
        MaPT = ma;
        TenHang = ten;
        NamSanXuat = nam;
        GiaGoc = gia;
    }

    public abstract decimal TinhGiaLanBanh();

    public virtual string GetInfo()
    {
        return "MaPT: " + MaPT +
               ", Ten hang: " + TenHang +
               ", Nam san xuat: " + NamSanXuat +
               ", Gia goc: " + GiaGoc.ToString("N0") + " VND";
    }
}

class OTo : PhuongTien
{
    private int _soChoNgoi;
    private double _dungTichDongCo;

    public int SoChoNgoi
    {
        get { return _soChoNgoi; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("So cho ngoi phai lon hon 0!");
            _soChoNgoi = value;
        }
    }

    public double DungTichDongCo
    {
        get { return _dungTichDongCo; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Dung tich dong co phai lon hon 0!");
            _dungTichDongCo = value;
        }
    }

    public OTo(string ma, string ten, int nam, decimal gia,
        int soCho, double dungTich)
        : base(ma, ten, nam, gia)
    {
        SoChoNgoi = soCho;
        DungTichDongCo = dungTich;
    }

    public override decimal TinhGiaLanBanh()
    {
        if (SoChoNgoi <= 9)
        {
            return GiaGoc +
                   GiaGoc * 0.12m +
                   GiaGoc * 0.30m;
        }

        return GiaGoc + GiaGoc * 0.10m;
    }

    public override string GetInfo()
    {
        return base.GetInfo() +
               ", So cho ngoi: " + SoChoNgoi +
               ", Dung tich dong co: " + DungTichDongCo;
    }
}

class XeMay : PhuongTien
{
    private int _dungTichXylanh;

    public int DungTichXylanh
    {
        get { return _dungTichXylanh; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Dung tich xylanh phai lon hon 0!");
            _dungTichXylanh = value;
        }
    }

    public XeMay(string ma, string ten, int nam, decimal gia, int cc)
        : base(ma, ten, nam, gia)
    {
        DungTichXylanh = cc;
    }

    public override decimal TinhGiaLanBanh()
    {
        if (DungTichXylanh < 175)
            return GiaGoc + GiaGoc * 0.02m;

        return GiaGoc + GiaGoc * 0.05m;
    }

    public override string GetInfo()
    {
        return base.GetInfo() +
               ", Dung tich xylanh: " + DungTichXylanh + " cc";
    }
}

class QuanLyPhuongTien
{
    private List<PhuongTien> danhSach;

    public QuanLyPhuongTien()
    {
        danhSach = new List<PhuongTien>();
    }

    public void AddPhuongTien(PhuongTien pt)
    {
        danhSach.Add(pt);
    }

    public void DisplayAll()
    {
        foreach (PhuongTien pt in danhSach)
        {
            Console.WriteLine(pt.GetInfo());
            Console.WriteLine("Gia lan banh: " +
                pt.TinhGiaLanBanh().ToString("N0") + " VND");
            Console.WriteLine();
        }
    }

    public PhuongTien FindMaxGiaLanBanh()
    {
        if (danhSach.Count == 0)
            return null;

        return danhSach
            .OrderByDescending(pt => pt.TinhGiaLanBanh())
            .First();
    }

    public List<PhuongTien> SearchByName(string keyword)
    {
        return danhSach
            .Where(pt => pt.TenHang.Contains(keyword))
            .ToList();
    }
}

class Program
{
    static void Main()
    {
        QuanLyPhuongTien ql = new QuanLyPhuongTien();

        Console.WriteLine("TC01 - KIEM TRA VALIDATION");

        try
        {
            OTo otoTest = new OTo(
                "OT01",
                "Toyota",
                1850,
                1000000000m,
                5,
                2.0);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine();

        Console.WriteLine("TC02 - TINH GIA LAN BANH O TO");

        OTo oto = new OTo(
            "OT01",
            "Toyota",
            2024,
            1000000000m,
            5,
            2.0);

        decimal giaOto = oto.TinhGiaLanBanh();

        Console.WriteLine(oto.GetInfo());
        Console.WriteLine("Gia lan banh: " +
            giaOto.ToString("N0") + " VND");

        Console.WriteLine();

        Console.WriteLine("TC03 - TINH GIA LAN BANH XE MAY");

        XeMay xeMay = new XeMay(
            "XM01",
            "Honda",
            2024,
            50000000m,
            150);

        decimal giaXeMay = xeMay.TinhGiaLanBanh();

        Console.WriteLine(xeMay.GetInfo());
        Console.WriteLine("Gia lan banh: " +
            giaXeMay.ToString("N0") + " VND");

        Console.WriteLine();

        Console.WriteLine("TC04 - KIEM TRA DA HINH");

        ql.AddPhuongTien(oto);
        ql.AddPhuongTien(xeMay);

        ql.DisplayAll();

        Console.WriteLine("TC05 - TIM GIA LAN BANH CAO NHAT");

        PhuongTien max = ql.FindMaxGiaLanBanh();

        if (max != null)
        {
            Console.WriteLine(max.GetInfo());
            Console.WriteLine("Gia lan banh: " +
                max.TinhGiaLanBanh().ToString("N0") + " VND");
        }

        Console.WriteLine();

        Console.WriteLine("SEARCH BY NAME - TOYOTA");

        List<PhuongTien> ketQua = ql.SearchByName("Toyota");

        foreach (PhuongTien pt in ketQua)
        {
            Console.WriteLine(pt.GetInfo());
        }

        Console.ReadKey();
    }
}