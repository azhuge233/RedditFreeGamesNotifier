# RedditFreeGamesNotifier

A CLI tool

- Fetch Steam, Ubisoft, itch.io and GOG free games info from 
  - [https://old.reddit.com/r/FreeGamesOnSteam/](https://old.reddit.com/r/FreeGamesOnSteam/)
  - [https://old.reddit.com/r/FreeGameFindings/](https://old.reddit.com/r/FreeGameFindings/)
  - [https://old.reddit.com/r/freegames/](https://old.reddit.com/r/freegames/) 
- Send notifications to Telegram, Bark, Email, QQ, PushPlus(Wechat), DingTalk, PushDeer and Discord.
- Auto claim detected Steam free games with ASF `addlicense` command.
- Auto claim detected GOG giveaways.

Demo Telegram Channel [@azhuge233_FreeGames](https://t.me/azhuge233_FreeGames)

## Build

Install dotnet 7.0 SDK first, you can find installation packages/guides [here](https://dotnet.microsoft.com/download).

```shell
git clone https://github.com/azhuge233/RedditFreeGamesNotifier.git
cd RedditFreeGamesNotifier
dotnet publish -c Release -o /your/path/here -r [win-x64/osx-x64/...] --sc
```

## Usage

Set your telegram bot token and chat ID in config.json.

Check [
SteamDB-FreeGames-dotnet wiki](https://github.com/azhuge233/SteamDB-FreeGames-dotnet/wiki/Config-Description) and [GOGGiveawayNotifier wiki](https://github.com/azhuge233/GOGGiveawayNotifier/wiki/Config-Description) for more config explanations and how to get GOG cookie, only notify, ASF and GOG varaibles are available for this project.

### Repeatedly running

The program will not add while/for loop, it's a scraper. To schedule the program, use cron.d in Linux(macOS) or Task Scheduler in Windows.

## My Free Games Collection

- SteamDB
    - [https://github.com/azhuge233/SteamDB-FreeGames-dotnet](https://github.com/azhuge233/SteamDB-FreeGames-dotnet)(Stop Maintained)
- EpicBundle
    - [https://github.com/azhuge233/EpicBundle-FreeGames-dotnet](https://github.com/azhuge233/EpicBundle-FreeGames-dotnet)
- Indiegala
    - [https://github.com/azhuge233/IndiegalaFreebieNotifier](https://github.com/azhuge233/IndiegalaFreebieNotifier)
- GOG
    - [https://github.com/azhuge233/GOGGiveawayNotifier](https://github.com/azhuge233/GOGGiveawayNotifier)
- Ubisoft
    - [https://github.com/azhuge233/UbisoftGiveawayNotifier](https://github.com/azhuge233/UbisoftGiveawayNotifier)
- PlayStation Plus
    - [https://github.com/azhuge233/PSPlusMonthlyGames-Notifier](https://github.com/azhuge233/PSPlusMonthlyGames-Notifier)
- Reddit Community
    - [https://github.com/azhuge233/RedditFreeGamesNotifier](https://github.com/azhuge233/RedditFreeGamesNotifier)