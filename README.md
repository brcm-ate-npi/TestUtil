# NPI Clotho Master

# TBU

Update:
- git tag v1.0.0
- git push origin v1.0.0

### https://npiftp-kor-wsd-01.kor.broadcom.net/clothoMaster-release/
---

## 5. How to Release a New Version (for Developers)

This project uses Squirrel.Windows for releases, managed by a `.nuspec` file.

**Prerequisites:**
- Download `nuget.exe (winget install --id Microsoft.NuGet)` and `Squirrel.exe` and ensure they are accessible from your command line/PATH.

**Release Steps:**

1.  **Update Version**: For a new release, edit `NPISpectreConverter.nuspec` and increment the `<version>` tag (e.g., `<version>1.0.1</version>`).

2.  **Build Project**: Build the solution in **Release** mode within Visual Studio.

3.  **Create NuGet Package**: From the project's root directory, run:
    ```shell
    nuget pack NPISpectreConverter.nuspec
    ```
    This creates a `.nupkg` file (e.g., `NPISpectreConverter.1.0.1.nupkg`).

4.  **Create Squirrel Release**: Run the `releasify` command on the newly created package:
    ```shell
    Squirrel.exe --releasify NPISpectreConverter.1.0.1.nupkg
    ```
    This will generate a `Releases` folder containing `Setup.exe` and the update packages.

5.  **Deploy**: Copy the contents of the `Releases` folder to the update location configured in the `UpdatePathFolder` key in `App.config`.
