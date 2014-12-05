@ECHO OFF

ECHO Packing GNaP.DependencyInjection.Ninject
nuget pack src\GNaP.DependencyInjection.Ninject\GNaP.DependencyInjection.Ninject.csproj -Build -Prop Configuration=Release -Exclude gnap.ico
