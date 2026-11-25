using System.Globalization;
using Application.Services;
using KousyoERP.Forms.Master;
using KousyoERP.Forms.Uriage;
using KousyoERP.Forms.Zaiko;

namespace KousyoERP.Forms;

public partial class FrmMainMenu : Form
{
    private readonly Func<IKuno0020Service> _kuno0020ServiceFactory;
    private readonly Func<IKuno0010Service> _kuno0010ServiceFactory;
    private readonly Func<IKuno0060Service> _kuno0060ServiceFactory;
    private readonly Func<IKuno0160Service> _kuno0160ServiceFactory;
    private readonly Func<IKzko0030Service> _kzko0030ServiceFactory;
    private readonly Func<IKsho0115Service> _ksho0115ServiceFactory;
    private readonly Func<IKzko0020Service> _kzko0020ServiceFactory;
    private readonly Func<IKzko0010Service> _kzko0010ServiceFactory;
    private readonly Func<IKuno0040Service> _kuno0040ServiceFactory;
    private readonly Func<IKuno0110Service> _kuno0110ServiceFactory;
    private readonly Func<IKtbo0020Service> _ktbo0020ServiceFactory;
    private FrmKUNO0020? _activeKuno0020;
    private FrmKUNO0010? _activeKuno0010;
    private FrmKUNO0060? _activeKuno0060;
    private FrmKUNO0160? _activeKuno0160;
    private FrmKUNO0040? _activeKuno0040;
    private FrmKUNO0110? _activeKuno0110;
    private FrmKZKO0030? _activeKzko0030;
    private FrmKSHO0115? _activeKsho0115;
    private FrmKZKO0020? _activeKzko0020;
    private FrmKZKO0010? _activeKzko0010;
    private FrmKTBO0020? _activeKtbo0020;

    public FrmMainMenu()
        : this(
            CreateDefaultKuno0020Service,
            CreateDefaultKuno0010Service,
            CreateDefaultKzko0030Service,
            CreateDefaultKuno0060Service,
            CreateDefaultKuno0160Service,
            CreateDefaultKsho0115Service,
            CreateDefaultKzko0020Service,
            CreateDefaultKzko0010Service,
            CreateDefaultKuno0040Service,
            CreateDefaultKuno0110Service,
            CreateDefaultKtbo0020Service)
    {
    }

    public FrmMainMenu(
        Func<IKuno0020Service> kuno0020ServiceFactory,
        Func<IKuno0010Service> kuno0010ServiceFactory,
        Func<IKzko0030Service>? kzko0030ServiceFactory = null,
        Func<IKuno0060Service>? kuno0060ServiceFactory = null,
        Func<IKuno0160Service>? kuno0160ServiceFactory = null,
        Func<IKsho0115Service>? ksho0115ServiceFactory = null,
        Func<IKzko0020Service>? kzko0020ServiceFactory = null,
        Func<IKzko0010Service>? kzko0010ServiceFactory = null,
        Func<IKuno0040Service>? kuno0040ServiceFactory = null,
        Func<IKuno0110Service>? kuno0110ServiceFactory = null,
        Func<IKtbo0020Service>? ktbo0020ServiceFactory = null)
    {
        _kuno0020ServiceFactory = kuno0020ServiceFactory;
        _kuno0010ServiceFactory = kuno0010ServiceFactory;
        _kzko0030ServiceFactory = kzko0030ServiceFactory ?? CreateDefaultKzko0030Service;
        _kuno0060ServiceFactory = kuno0060ServiceFactory ?? CreateDefaultKuno0060Service;
        _kuno0160ServiceFactory = kuno0160ServiceFactory ?? CreateDefaultKuno0160Service;
        _ksho0115ServiceFactory = ksho0115ServiceFactory ?? CreateDefaultKsho0115Service;
        _kzko0020ServiceFactory = kzko0020ServiceFactory ?? CreateDefaultKzko0020Service;
        _kzko0010ServiceFactory = kzko0010ServiceFactory ?? CreateDefaultKzko0010Service;
        _kuno0040ServiceFactory = kuno0040ServiceFactory ?? CreateDefaultKuno0040Service;
        _kuno0110ServiceFactory = kuno0110ServiceFactory ?? CreateDefaultKuno0110Service;
        _ktbo0020ServiceFactory = ktbo0020ServiceFactory ?? CreateDefaultKtbo0020Service;
        InitializeComponent();
    }

    private void FrmMainMenu_Load(object? sender, EventArgs e)
    {
        lblDate.Text = DateTime.Now.ToString("yy/MM/dd", CultureInfo.InvariantCulture);
    }

    private void BtnExit_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private static IKuno0020Service CreateDefaultKuno0020Service()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Kuno0020Service(
            new Infrastructure.Repositories.KmBnriRepository(factory),
            new Infrastructure.Repositories.KmMeisRepository(factory));
    }

    private static IKuno0010Service CreateDefaultKuno0010Service()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Kuno0010Service(
            new Infrastructure.Repositories.KmSyofRepository(factory),
            new Infrastructure.Repositories.KmBnriRepository(factory),
            new Infrastructure.Repositories.KmMeisRepository(factory));
    }

    private static IKuno0060Service CreateDefaultKuno0060Service()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Kuno0060Service(
            new Infrastructure.Repositories.KmSyzkRepository(factory),
            new Infrastructure.Repositories.KmMeisRepository(factory),
            new Infrastructure.Repositories.KmGrpfRepository(factory));
    }

    private static IKuno0160Service CreateDefaultKuno0160Service()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Kuno0160Service(
            new Infrastructure.Repositories.KmTantRepository(factory),
            new Infrastructure.Repositories.KmMeisRepository(factory));
    }

    private static IKzko0030Service CreateDefaultKzko0030Service()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Kzko0030Service(
            new Infrastructure.Repositories.KmZaikoRepository(factory),
            new Infrastructure.Repositories.KmSyofRepository(factory),
            new Infrastructure.Repositories.KmBnriRepository(factory),
            new Infrastructure.Repositories.KmMeisRepository(factory));
    }

    private static IKsho0115Service CreateDefaultKsho0115Service()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Ksho0115Service(
            new Infrastructure.Repositories.VtUjnlRepository(factory),
            new Infrastructure.Repositories.KmBnriRepository(factory),
            new Infrastructure.Repositories.KmKyakRepository(factory));
    }

    private static IKzko0020Service CreateDefaultKzko0020Service()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Kzko0020Service(
            new Infrastructure.Repositories.KmLockRepository(factory),
            new Infrastructure.Repositories.KtZjnlRepository(factory),
            new Infrastructure.Repositories.KmTantRepository(factory),
            new Infrastructure.Repositories.KmSyofRepository(factory),
            new Infrastructure.Repositories.KmBnriRepository(factory),
            new Infrastructure.Repositories.KmMeisRepository(factory));
    }

    private static IKzko0010Service CreateDefaultKzko0010Service()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Kzko0010Service(
            new Infrastructure.Repositories.KmLockRepository(factory),
            new Infrastructure.Repositories.KmMeisRepository(factory),
            new Infrastructure.Repositories.KmSyofRepository(factory),
            new Infrastructure.Repositories.KmZaikoRepository(factory));
    }

    private static IKuno0040Service CreateDefaultKuno0040Service()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Kuno0040Service(
            new Infrastructure.Repositories.KmKyakRepository(factory),
            new Infrastructure.Repositories.KmGrpfRepository(factory),
            new Infrastructure.Repositories.KmSyzkRepository(factory),
            new Infrastructure.Repositories.KmMeisRepository(factory),
            new Infrastructure.Repositories.KmGnkoRepository(factory));
    }

    private static IKuno0110Service CreateDefaultKuno0110Service()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Kuno0110Service(new Infrastructure.Repositories.KmMeisRepository(factory));
    }

    private static IKtbo0020Service CreateDefaultKtbo0020Service()
    {
        var factory = new Infrastructure.Data.OracleConnectionFactory();
        return new Ktbo0020Service(
            new Infrastructure.Repositories.KtUjnlRepository(factory),
            new Infrastructure.Repositories.KmKyakRepository(factory),
            new Infrastructure.Repositories.KmTantRepository(factory),
            new Infrastructure.Repositories.KmBnriRepository(factory),
            new Infrastructure.Repositories.KmSyofRepository(factory),
            new Infrastructure.Repositories.KmMeisRepository(factory));
    }

    private void BtnKUNO0020_Click(object? sender, EventArgs e)
    {
        // ２重起動防止 (VB6 App.PrevInstance 相当)
        if (_activeKuno0020 is { IsDisposed: false })
        {
            _activeKuno0020.Activate();
            return;
        }

        Hide();
        try
        {
            using var frm = new FrmKUNO0020(_kuno0020ServiceFactory());
            _activeKuno0020 = frm;
            frm.ShowDialog(this);
        }
        finally
        {
            _activeKuno0020 = null;
            if (!IsDisposed)
            {
                Show();
                Activate();
            }
        }
    }

    private void BtnKUNO0010_Click(object? sender, EventArgs e)
    {
        // ２重起動防止 (VB6 App.PrevInstance 相当)
        if (_activeKuno0010 is { IsDisposed: false })
        {
            _activeKuno0010.Activate();
            return;
        }

        Hide();
        try
        {
            using var frm = new FrmKUNO0010(_kuno0010ServiceFactory());
            _activeKuno0010 = frm;
            frm.ShowDialog(this);
        }
        finally
        {
            _activeKuno0010 = null;
            if (!IsDisposed)
            {
                Show();
                Activate();
            }
        }
    }

    private void BtnKUNO0060_Click(object? sender, EventArgs e)
    {
        // ２重起動防止 (VB6 App.PrevInstance 相当)
        if (_activeKuno0060 is { IsDisposed: false })
        {
            _activeKuno0060.Activate();
            return;
        }

        Hide();
        try
        {
            using var frm = new FrmKUNO0060(_kuno0060ServiceFactory());
            _activeKuno0060 = frm;
            frm.ShowDialog(this);
        }
        finally
        {
            _activeKuno0060 = null;
            if (!IsDisposed)
            {
                Show();
                Activate();
            }
        }
    }

    private void BtnKUNO0160_Click(object? sender, EventArgs e)
    {
        // ２重起動防止 (VB6 App.PrevInstance 相当)
        if (_activeKuno0160 is { IsDisposed: false })
        {
            _activeKuno0160.Activate();
            return;
        }

        Hide();
        try
        {
            using var frm = new FrmKUNO0160(_kuno0160ServiceFactory());
            _activeKuno0160 = frm;
            frm.ShowDialog(this);
        }
        finally
        {
            _activeKuno0160 = null;
            if (!IsDisposed)
            {
                Show();
                Activate();
            }
        }
    }

    private void BtnKZKO0030_Click(object? sender, EventArgs e)
    {
        // ２重起動防止 (VB6 App.PrevInstance 相当)
        if (_activeKzko0030 is { IsDisposed: false })
        {
            _activeKzko0030.Activate();
            return;
        }

        Hide();
        try
        {
            using var frm = new FrmKZKO0030(_kzko0030ServiceFactory());
            _activeKzko0030 = frm;
            frm.ShowDialog(this);
        }
        finally
        {
            _activeKzko0030 = null;
            if (!IsDisposed)
            {
                Show();
                Activate();
            }
        }
    }

    private void BtnKSHO0115_Click(object? sender, EventArgs e)
    {
        // ２重起動防止 (VB6 App.PrevInstance 相当)
        if (_activeKsho0115 is { IsDisposed: false })
        {
            _activeKsho0115.Activate();
            return;
        }

        Hide();
        try
        {
            using var frm = new FrmKSHO0115(_ksho0115ServiceFactory());
            _activeKsho0115 = frm;
            frm.ShowDialog(this);
        }
        finally
        {
            _activeKsho0115 = null;
            if (!IsDisposed)
            {
                Show();
                Activate();
            }
        }
    }

    private void BtnKZKO0020_Click(object? sender, EventArgs e)
    {
        // ２重起動防止 (VB6 App.PrevInstance 相当)
        if (_activeKzko0020 is { IsDisposed: false })
        {
            _activeKzko0020.Activate();
            return;
        }

        Hide();
        try
        {
            using var frm = new FrmKZKO0020(_kzko0020ServiceFactory());
            _activeKzko0020 = frm;
            frm.ShowDialog(this);
        }
        finally
        {
            _activeKzko0020 = null;
            if (!IsDisposed)
            {
                Show();
                Activate();
            }
        }
    }

    private void BtnKZKO0010_Click(object? sender, EventArgs e)
    {
        // ２重起動防止 (VB6 App.PrevInstance 相当)
        if (_activeKzko0010 is { IsDisposed: false })
        {
            _activeKzko0010.Activate();
            return;
        }

        Hide();
        try
        {
            using var frm = new FrmKZKO0010(_kzko0010ServiceFactory());
            _activeKzko0010 = frm;
            frm.ShowDialog(this);
        }
        finally
        {
            _activeKzko0010 = null;
            if (!IsDisposed)
            {
                Show();
                Activate();
            }
        }
    }

    private void BtnKUNO0040_Click(object? sender, EventArgs e)
    {
        // ２重起動防止 (VB6 App.PrevInstance 相当)
        if (_activeKuno0040 is { IsDisposed: false })
        {
            _activeKuno0040.Activate();
            return;
        }

        Hide();
        try
        {
            using var frm = new FrmKUNO0040(_kuno0040ServiceFactory());
            _activeKuno0040 = frm;
            frm.ShowDialog(this);
        }
        finally
        {
            _activeKuno0040 = null;
            if (!IsDisposed)
            {
                Show();
                Activate();
            }
        }
    }

    private void BtnKUNO0110_Click(object? sender, EventArgs e)
    {
        // ２重起動防止 (VB6 App.PrevInstance 相当)
        if (_activeKuno0110 is { IsDisposed: false })
        {
            _activeKuno0110.Activate();
            return;
        }

        Hide();
        try
        {
            using var frm = new FrmKUNO0110(_kuno0110ServiceFactory());
            _activeKuno0110 = frm;
            frm.ShowDialog(this);
        }
        finally
        {
            _activeKuno0110 = null;
            if (!IsDisposed)
            {
                Show();
                Activate();
            }
        }
    }

    private void BtnKTBO0020_Click(object? sender, EventArgs e)
    {
        // ２重起動防止 (VB6 App.PrevInstance 相当)
        if (_activeKtbo0020 is { IsDisposed: false })
        {
            _activeKtbo0020.Activate();
            return;
        }

        Hide();
        try
        {
            using var frm = new FrmKTBO0020(_ktbo0020ServiceFactory());
            _activeKtbo0020 = frm;
            frm.ShowDialog(this);
        }
        finally
        {
            _activeKtbo0020 = null;
            if (!IsDisposed)
            {
                Show();
                Activate();
            }
        }
    }
}
