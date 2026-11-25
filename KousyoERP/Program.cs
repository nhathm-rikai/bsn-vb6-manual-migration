using System.Text;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using KousyoERP.Forms;
using KousyoERP.Forms.Master;
using KousyoERP.Forms.Uriage;
using KousyoERP.Forms.Zaiko;

namespace KousyoERP;

internal static class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        // Shift-JIS (CP932) エンコーディング登録 (FUNC_CHK_MOJISU 等で使用)
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        // ２重起動チェック (VB6 App.PrevInstance 相当)
        using var mutex = new Mutex(true, "Global\\KousyoERP_SingleInstance", out bool createdNew);
        if (!createdNew)
        {
            return;
        }

        ApplicationConfiguration.Initialize();

        var connectionFactory = new OracleConnectionFactory();
        var kmBnriRepo = new KmBnriRepository(connectionFactory);
        var kmMeisRepo = new KmMeisRepository(connectionFactory);
        var kmSyofRepo = new KmSyofRepository(connectionFactory);
        var kmZaikoRepo = new KmZaikoRepository(connectionFactory);
        var kmSyzkRepo = new KmSyzkRepository(connectionFactory);
        var kmGrpfRepo = new KmGrpfRepository(connectionFactory);
        var kmTantRepo = new KmTantRepository(connectionFactory);
        var vtUjnlRepo = new VtUjnlRepository(connectionFactory);
        var kmKyakRepo = new KmKyakRepository(connectionFactory);
        var kmLockRepo = new KmLockRepository(connectionFactory);
        var ktZjnlRepo = new KtZjnlRepository(connectionFactory);
        var kmGnkoRepo = new KmGnkoRepository(connectionFactory);
        var ktUjnlRepo = new KtUjnlRepository(connectionFactory);

        IKuno0020Service CreateKuno0020Service() => new Kuno0020Service(kmBnriRepo, kmMeisRepo);
        IKuno0010Service CreateKuno0010Service() => new Kuno0010Service(kmSyofRepo, kmBnriRepo, kmMeisRepo);
        IKuno0060Service CreateKuno0060Service() => new Kuno0060Service(kmSyzkRepo, kmMeisRepo, kmGrpfRepo);
        IKuno0160Service CreateKuno0160Service() => new Kuno0160Service(kmTantRepo, kmMeisRepo);
        IKuno0040Service CreateKuno0040Service() => new Kuno0040Service(kmKyakRepo, kmGrpfRepo, kmSyzkRepo, kmMeisRepo, kmGnkoRepo);
        IKuno0110Service CreateKuno0110Service() => new Kuno0110Service(kmMeisRepo);
        IKzko0030Service CreateKzko0030Service() => new Kzko0030Service(kmZaikoRepo, kmSyofRepo, kmBnriRepo, kmMeisRepo);
        IKsho0115Service CreateKsho0115Service() => new Ksho0115Service(vtUjnlRepo, kmBnriRepo, kmKyakRepo);
        IKzko0020Service CreateKzko0020Service() => new Kzko0020Service(kmLockRepo, ktZjnlRepo, kmTantRepo, kmSyofRepo, kmBnriRepo, kmMeisRepo);
        IKzko0010Service CreateKzko0010Service() => new Kzko0010Service(kmLockRepo, kmMeisRepo, kmSyofRepo, kmZaikoRepo);
        IKtbo0020Service CreateKtbo0020Service() => new Ktbo0020Service(ktUjnlRepo, kmKyakRepo, kmTantRepo, kmBnriRepo, kmSyofRepo, kmMeisRepo);

        // コマンドライン引数 `--screen=KUNO0020` / `--screen=KUNO0010` / `--screen=KUNO0060` / `--screen=KUNO0160` / `--screen=KZKO0030` / `--screen=KSHO0115` / `--screen=KZKO0020` / `--screen=KZKO0010` 指定時は直接該当画面を起動
        string? screenArg = args
            .FirstOrDefault(a => a.StartsWith("--screen=", StringComparison.OrdinalIgnoreCase))
            ?.Split('=', 2)[1];

        if (string.Equals(screenArg, "KUNO0020", StringComparison.OrdinalIgnoreCase))
        {
            System.Windows.Forms.Application.Run(new FrmKUNO0020(CreateKuno0020Service()));
            return;
        }

        if (string.Equals(screenArg, "KUNO0010", StringComparison.OrdinalIgnoreCase))
        {
            System.Windows.Forms.Application.Run(new FrmKUNO0010(CreateKuno0010Service()));
            return;
        }

        if (string.Equals(screenArg, "KUNO0060", StringComparison.OrdinalIgnoreCase))
        {
            System.Windows.Forms.Application.Run(new FrmKUNO0060(CreateKuno0060Service()));
            return;
        }

        if (string.Equals(screenArg, "KUNO0160", StringComparison.OrdinalIgnoreCase))
        {
            System.Windows.Forms.Application.Run(new FrmKUNO0160(CreateKuno0160Service()));
            return;
        }

        if (string.Equals(screenArg, "KZKO0030", StringComparison.OrdinalIgnoreCase))
        {
            System.Windows.Forms.Application.Run(new FrmKZKO0030(CreateKzko0030Service()));
            return;
        }

        if (string.Equals(screenArg, "KSHO0115", StringComparison.OrdinalIgnoreCase))
        {
            System.Windows.Forms.Application.Run(new FrmKSHO0115(CreateKsho0115Service()));
            return;
        }

        if (string.Equals(screenArg, "KZKO0020", StringComparison.OrdinalIgnoreCase))
        {
            System.Windows.Forms.Application.Run(new FrmKZKO0020(CreateKzko0020Service()));
            return;
        }

        if (string.Equals(screenArg, "KZKO0010", StringComparison.OrdinalIgnoreCase))
        {
            System.Windows.Forms.Application.Run(new FrmKZKO0010(CreateKzko0010Service()));
            return;
        }

        if (string.Equals(screenArg, "KUNO0040", StringComparison.OrdinalIgnoreCase))
        {
            System.Windows.Forms.Application.Run(new FrmKUNO0040(CreateKuno0040Service()));
            return;
        }

        if (string.Equals(screenArg, "KUNO0110", StringComparison.OrdinalIgnoreCase))
        {
            System.Windows.Forms.Application.Run(new FrmKUNO0110(CreateKuno0110Service()));
            return;
        }

        if (string.Equals(screenArg, "KTBO0020", StringComparison.OrdinalIgnoreCase))
        {
            System.Windows.Forms.Application.Run(new FrmKTBO0020(CreateKtbo0020Service()));
            return;
        }

        System.Windows.Forms.Application.Run(new FrmMainMenu(
            CreateKuno0020Service,
            CreateKuno0010Service,
            CreateKzko0030Service,
            CreateKuno0060Service,
            CreateKuno0160Service,
            CreateKsho0115Service,
            CreateKzko0020Service,
            CreateKzko0010Service,
            CreateKuno0040Service,
            CreateKuno0110Service,
            CreateKtbo0020Service));
    }
}
