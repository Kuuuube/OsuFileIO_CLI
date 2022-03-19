# OsuFileIO CLI

Jank CLI for [OsuFileIO](https://github.com/Xarib/OsuFileIO) to output analysis as json.

## Downloads

Downloads with are available in [Releases](https://github.com/Kuuuube/osuSkills_Scripts/releases).

## Usage

Either run with or without command line args.

## Command line args:

```
OsuFileIO_CLI {input} {output}
```

Using `checksum` for output will use the map's MD5 hash as the output file name.

## Dependencies

.Net Runtime 6.0 x64: https://dotnet.microsoft.com/en-us/download/dotnet/6.0

## Building:

Run `build.ps1` or manually run:

```
$options= @('--configuration', 'Release', '-p:PublishSingleFile=true', '-p:DebugType=embedded', '--self-contained', 'false')
dotnet publish OsuFileIO_CLI $options --runtime win-x64 --framework net6.0 -o build/win-x64
dotnet publish OsuFileIO_CLI $options --runtime linux-x64 --framework net6.0 -o build/linux-x64
```