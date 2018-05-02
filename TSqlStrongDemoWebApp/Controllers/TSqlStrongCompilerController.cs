﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using TSqlStrong;

namespace TSqlStrongDemoWebApp.Controllers
{
    [Route("api/TSqlStrongCompiler")]
    public class TSqlStrongCompilerController : Controller
    {
        [HttpPost("Compile")]
        public TSqlCompilationResult Compile([FromBody] CompileRequest request)            
        {
            return new TSqlCompilationResult()
            {
                CompiledTime = DateTime.Now.ToShortTimeString(),
                Issues = TSqlStrong.Ast.TypeChecker.Parse(request.Sql).Select(TSqlStrong.VerificationResults.Issue.Select.ToDTO(String.Empty)).ToArray(),
                Sql = request.Sql
            };
        }
    }

    public class CompileRequest
    {
        public string Sql { get; set; }
    }

    public class TSqlCompilationResult
    {
        public string CompiledTime { get; set; }
        public string Sql { get; set; }
        public TSqlStrong.VerificationResults.IssueDTO[] Issues { get; set; }
    }
}
