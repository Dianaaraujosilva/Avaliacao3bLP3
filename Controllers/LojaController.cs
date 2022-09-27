using MvcIntroViews.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MvcIntroViews.Controllers;

public class LojaController : Controller
{
    public static List<LojaViewModel> lojas = new List<LojaViewModel>();

    public IActionResult Index()
    {
        return View(lojas);
    }

    public IActionResult Admin()
    {
        return View(lojas);
    }

    public IActionResult Detalhes(int id)
    {
        return View(lojas[id - 1]);
    }

    public IActionResult Excluir(int id)
    {
        lojas.RemoveAt(id - 1);

        for(var i = id - 1; i < lojas.Count; i++)
        {
            lojas[i].Id -= 1;
        }

        return View();
    }

    public IActionResult Cadastrar()
    {
        return View();
    }

    public IActionResult ConfirmacaoCadastro([FromForm] string piso, [FromForm] string nome, [FromForm] string descricao, [FromForm] string tipo, [FromForm] string email)
    {
        foreach (var loja in lojas)
        {
            if(nome == loja.Nome)
            {
                ViewData["Titulo"] = "Cadastro negado!";
                ViewData["Mensagem"] = "A loja já é cadastrado";
                return View();
            }
        }

        int id = 1;
        if(lojas.Count != 0) id = lojas[lojas.Count - 1].Id + 1;

        lojas.Add(new LojaViewModel(id, piso, nome, descricao, tipo, email));

        ViewData["Titulo"] = "Cadastro aprovado!";
        ViewData["Mensagem"] = "A Loja foi cadastrada com sucesso";

        return View();
    }
}