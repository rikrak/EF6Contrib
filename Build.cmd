@ECHO OFF

%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\msbuild "%~dp0\EF.Contrib.msbuild" /v:minimal /maxcpucount /nodeReuse:false %*
