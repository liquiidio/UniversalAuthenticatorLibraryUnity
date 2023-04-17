




<div align="center">
 <img src="https://avatars.githubusercontent.com/u/82725791?s=200&v=4" align="center"
     alt="Liquiid logo" width="300" height="300">
</div>

---
 
# Universal Authenticator Library Sharp (UAL)

A native UAL allowing the use of supported SignatureProviders, similar to the js-based UAL allowing developers and users the same interaction flow and UI/UX on all different platforms. The priority of this plugin is for user and developer experience, while building the same base with extension-capabilities allowing to support additional SignatureProviders like Wombat, MetaMask, AIKON, other Wallets or SideChain-Auth in the future.

# Installation 

**_Requires Unity 2019.1+ with .NET 4.x+ Runtime_**

This package can be included into your project by either:

 1. Installing the package via Unity's Package Manager (UPM) in the editor (recommended).
 2. Importing the .unitypackage which you can download [here](https://github.com/liquiidio/UniversalAuthenticatorLibrarySharp/releases/latest/download/universalauthenticatorlibrarysharp.unitypackage). 
 3. Manually add the files in this repo.
 4. Installing it via NuGet.
---
### 1. Installing via Unity Package Manager (UPM).
In your Unity project:
 1. Open the Package Manager Window/Tab

    ![image](https://user-images.githubusercontent.com/74650011/208429048-37e2277c-3e10-4794-97e7-3ec87f55f8c9.png)

 2. Click on + icon and then click on "Add Package From Git URL"

    ![image](https://user-images.githubusercontent.com/74650011/208429298-76fe1101-95f3-4ab0-bbd5-f0a32a1cc652.png)

 3. Enter URL:  `https://github.com/liquiidio/UniversalAuthenticatorLibrarySharp.git#upm`
---
### 2. Importing the Unity Package.

Download the [UnityPackage here](https://github.com/liquiidio/UniversalAuthenticatorLibrarySharp/releases/latest/download/universalauthenticatorlibrarysharp.unitypackage). 

Then in your Unity project:

 1. Open up the import a custom package window
    
    ![image](https://user-images.githubusercontent.com/74650011/208430044-caf91dd9-111e-4224-8441-95d116dbec3b.png)

 2. Navigate to where you downloaded the file and open it.
 
    ![image](https://user-images.githubusercontent.com/86061433/217196170-c3bd43cb-488c-4243-a992-8ae0ee9c15fe.jpg)


 3. Check all the relevant files needed (if this is a first time import, just select ALL) and click on import.
 
   
     ![image](https://user-images.githubusercontent.com/86061433/217196421-7524f5e9-d0af-4d6e-9e42-3a16e34cd4a7.jpg)
     
---

### 3. Install manually. 
Download this [the latest Release](https://github.com/liquiidio/UniversalAuthenticatorLibrarySharp/releases/latest).

Then in your Unity project, copy the sources from `UniversalAuthenticatorLibrarySharp` into your Unity `Assets` directory.


---

# Dependencies
None of the dependencies is contained in this Package and no matter which installation method you choose, you have to install it manually in addition to this Package.

## EosSharp
EosSharp is a library containing the necessary functionallity to serialize and deserialize Actions, Transactions, Blocks and other Data
In addition it contains the necessary functionallity for all kinds of cryptographic operations
Lastly it contains the functionallity allowing you and the AnchorLink-Library to access EOSIO or LEAP-based Nodes via their APIs.

*EosSharp installation*

Follow the Instructions in [the EosSharp Repository](https://github.com/liquiidio/EosSharp)

*Or install the Package directly via UPM*
Installing via Unity Package Manager (UPM).
In your Unity project:
Open the Package Manager Window/Tab
Click Add Package From Git URL
Enter URL: https://github.com/liquiidio/EosSharp.git#upm


## AnchorLinkSharp
Allows users and developers to connect and communicate with Anchor Wallet and ESR-based applications. The Anchor & ESR Integration consists of multiple libraries for the ESR-Protocol, the Anchor-integration, Transports among others.

*AnchorLinkSharp package installation*

Follow the Instructions in [the AnchorLinkSharp Repository](https://github.com/liquiidio/AnchorLinkSharp)

*Or install the Package directly via UPM*
Installing via Unity Package Manager (UPM).
In your Unity project:
Open the Package Manager Window/Tab
Click Add Package From Git URL
Enter URL: https://github.com/liquiidio/AnchorLinkSharp.git#upm


## WaxCloudWallet (WCW)
A combination of local HttpListeners receiving OAuth-Callbacks from WCW-related web-adresses opened through the WebView-Plugin, gathering necessary initial information like OAuth-Tokens, followed by regular non-browser-based (non-WebView required) communication with the WCW-API/Server.

*WCW package installation*
Follow the Instructions in [the WCWUnity Repository](https://github.com/liquiidio/WcwUnity)

*Or install the Package directly via UPM*
Installing via Unity Package Manager (UPM).
In your Unity project:
Open the Package Manager Window/Tab
Click Add Package From Git URL
Enter URL: https://github.com/liquiidio/WcwUnity.git#upm

# Examples 

## Canvas 

1. In a Unity scene, add the Canvas authenticator handler prefab i.e. [Canvas](https://github.com/liquiidio/UniversalAuthenticatorLibrarySharp/blob/upm_full/Src/Canvas/Prefabs/UnityCanvasUAL.prefab).

2. Add the specific [authenticators](https://github.com/liquiidio/UniversalAuthenticatorLibrarySharp/tree/upm_full/Src/Authenticators) that will be used e.g. [AnchorWallet](https://github.com/liquiidio/UniversalAuthenticatorLibrarySharp/tree/upm_full/Src/Authenticators/Anchor/Prefabs) or [WAX Cloud Wallet](https://github.com/liquiidio/UniversalAuthenticatorLibrarySharp/tree/upm_full/Src/Authenticators/WaxCloudWallet/Prefabs) as prefabs to the scene and specify them in the handler that was added in Step 1.

3. Create a new script inheriting from MonoBehaviour, add a public member of type `UnityCanvasUAL` and assign it in the editor.

4. In the Start-method, instantiate/initialize the handler prefab added above and add an action when the user login is successful.

**N.B. Ensure that there is an assign event handler/callback for the login to get the `User` object. Click [here](https://liquiidio.gitbook.io/unity-plugin-suite/v/universalauthenticatorlibrary/examples/example_b) for more information.**

#### Canvas 
```csharp
   UnityCanvasUAL.OnUserLogin += UserLogin;
   await UnityCanvasUAL.Init();
```

## UI Toolkit

1. In a Unity scene, add the authenticator handler prefab i.e. [UIToolkit](https://github.com/liquiidio/UniversalAuthenticatorLibrarySharp/blob/upm_full/Src/UiToolkit/Prefabs/UnityUiToolkitUAL.prefab) .

2. Add the specific [authenticators](https://github.com/liquiidio/UniversalAuthenticatorLibrarySharp/tree/upm_full/Src/Authenticators) that will be used e.g. [AnchorWallet](https://github.com/liquiidio/UniversalAuthenticatorLibrarySharp/tree/upm_full/Src/Authenticators/Anchor/Prefabs) or [WAX Cloud Wallet](https://github.com/liquiidio/UniversalAuthenticatorLibrarySharp/tree/upm_full/Src/Authenticators/WaxCloudWallet/Prefabs) as prefabs to the scene and specify them in the handler that was added in Step 1.

3. Create a new script inheriting from MonoBehaviour, add a public member of type `UnityUiToolkitUAL` and assign it in the editor.

4. In the Start-method, instantiate/initialize the respective handler prefab added above and add an action when the user login is successful.

**N.B. Ensure that there is an assign event handler/callback for the login to get the `User` object. Click [here](https://liquiidio.gitbook.io/unity-plugin-suite/v/universalauthenticatorlibrary/examples/example_b) for more information.**

#### Canvas 
```csharp
   UnityCanvasUAL.OnUserLogin += UserLogin;
   await UnityCanvasUAL.Init();
```


## User 

To get the user that has logged in and to use the data to sign a transaction do the following:

1. After setting up a scene as explained [here](https://liquiidio.gitbook.io/unity-plugin-suite/v/universalauthenticatorlibrary/examples/examples_getting_started), add a memeber of type `User`

2. Create a new method (which should match the one called by the action in the [getting started](https://liquiidio.gitbook.io/unity-plugin-suite/v/universalauthenticatorlibrary/examples/examples_getting_started) section) and pass a `User` parameter.

3. Then assign the parameter to the variable of type `User`.

This is most effective when assigned as an event handler/callback .

```csharp
UnityXXXUal.OnUserLogin += [UserLoggedInMethod];
```

## Transfer tokens

To perform a transfer action, do the following (ensure that you have a `User` member assigned as shown [here](https://liquiidio.gitbook.io/unity-plugin-suite/v/universalauthenticatorlibrary/examples/example_b):

```csharp
   public async Task Transfer(string frmAcc, string toAcc, string qnty, string memo)
        {
            var action = new EosSharp.Core.Api.v1.Action
            {
                account = "eosio.token",
                name = "transfer",
                authorization = new List<PermissionLevel>
                {
                    new PermissionLevel()
                    {
                        actor =
                            "............1", // ............1 will be resolved to the signing accounts permission
                        permission =
                            "............2" // ............2 will be resolved to the signing accounts authority
                    }
                },

                data = new Dictionary<string, object>
                {
                    {"from", frmAcc},
                    {"to", toAcc},
                    {"quantity", qnty},
                    {"memo", memo}
                }
            };
            try
            {
                await user.SignTransaction(new[] {action});
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
        }
```
## Transaction

To perform a generic transaction, do the following (ensure that you have a `User` member assigned as shown [here](https://liquiidio.gitbook.io/unity-plugin-suite/v/universalauthenticatorlibrary/examples/example_b):

```csharp
   public async Task Transact(EosSharp.Core.Api.v1.Action action)
        {
            var transactResult = await User.SignTransaction(new []{ action });
        }
```

