﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RCM.Application.ApplicationInterfaces;
using RCM.Application.ViewModels;
using RCM.Domain.DomainNotificationHandlers;
using RCM.Domain.Models.FornecedorModels;
using RCM.Presentation.Web.Controllers;
using System;

namespace RCM.Presentation.Web.Areas.Platform.Controllers
{
    [Authorize]
    [Area("Platform")]
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorApplicationService _fornecedorApplicationService;

        public FornecedoresController(IFornecedorApplicationService fornecedorApplicationService, IDomainNotificationHandler domainNotificationHandler) :
                                                                                                                                base(domainNotificationHandler)
        {
            _fornecedorApplicationService = fornecedorApplicationService;
        }

        public IActionResult Index(string nome = null)
        {
            var nomeSpecification = new FornecedorNomeSpecification(nome);

            var list = _fornecedorApplicationService.Get(nomeSpecification
                .ToExpression());

            return View(list);
        }

        public IActionResult Details(Guid id)
        {
            var fornecedor = _fornecedorApplicationService.GetById(id);
            if (fornecedor == null)
                return NotFound();

            return View(fornecedor);
        }

        [Authorize(Policy = "ActiveUser")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "ActiveUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FornecedorViewModel fornecedor)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return View(fornecedor);
            }

            _fornecedorApplicationService.Add(fornecedor);

            if (Success())
                return RedirectToAction(nameof(Index));
            else
                return View(fornecedor);
        }

        [Authorize(Policy = "ActiveUser")]
        public IActionResult Edit(Guid id)
        {
            var fornecedor = _fornecedorApplicationService.GetById(id);
            if (fornecedor == null)
                return NotFound();

            return View(fornecedor);
        }

        [Authorize(Policy = "ActiveUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, FornecedorViewModel fornecedor)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return View(fornecedor);
            }

            _fornecedorApplicationService.Update(fornecedor);

            if (Success())
                return RedirectToAction(nameof(Index));
            else
                return View(fornecedor);
        }

        [Authorize(Policy = "ActiveUser")]
        public IActionResult Delete(Guid id)
        {
            var fornecedor = _fornecedorApplicationService.GetById(id);
            if (fornecedor == null)
                return NotFound();

            return View(fornecedor);
        }

        [Authorize(Policy = "ActiveUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id, FornecedorViewModel fornecedor)
        {
            _fornecedorApplicationService.Remove(fornecedor);

            if (Success())
                return RedirectToAction(nameof(Index));

            return View(fornecedor);
        }
    }
}