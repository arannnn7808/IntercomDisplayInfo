# IntercomDisplayInfo

[![LabAPI](https://img.shields.io/badge/LabAPI-1.1.1+-blue)](https://github.com/northwood-studios/LabAPI)
[![GitHub all releases](https://img.shields.io/github/downloads/arannnn7808/IntercomDisplayInfo/total)](https://github.com/arannnn7808/IntercomDisplayInfo/releases)
[![GitHub forks](https://img.shields.io/github/forks/arannnn7808/IntercomDisplayInfo)](https://github.com/arannnn7808/IntercomDisplayInfo/network/members)
[![GitHub](https://img.shields.io/github/license/arannnn7808/IntercomDisplayInfo)](https://github.com/arannnn7808/IntercomDisplayInfo/blob/master/LICENSE)

Enhances the Intercom display, turning it into a fully customizable, live information panel. It shows vital round statistics at a glance, including the round timer, player counts for each team, and the current Intercom status.

## Features

-   **Live Round Timer:** Displays the current match time directly on the Intercom screen.
-   **Team Counters:** Shows the number of living players for Class-D, Scientists, Foundation Forces, Chaos Insurgency, and SCPs.
-   **Real-Time Intercom Status:** Provides clear status updates (e.g., Ready, In Use, Cooldown).
-   **Fully Configurable:** Enable or disable each piece of information via a simple config file.
-   **Translation Support:** Customize all displayed text and labels to fit your server's language or style.

## Dependencies

-   **LabAPI v1.1.1** or newer is required for this plugin.
-   **0Harmony**

## Installation

1.  Make sure you have **LabAPI v1.1.1** or a compatible version installed on your server.
2.  Download the latest release of `IntercomDisplayInfo.dll` from the [**Releases Page**](https://github.com/arannnn7808/IntercomDisplayInfo/releases).
3.  Place the downloaded `.dll` file into your server's plugin directory (`.config/SCP Secret Laboratory/LabApi/plugins/(server-port/global)`).
4.  Download dependencies zip, extract it and place the content into (`.config/SCP Secret Laboratory/LabApi/dependencies/(server-port/global)`).
5.  Restart the server. The configuration and translation files will be generated on the first run.

## Configuration

After the first launch, two files will be created in your LabApi configuration folder: `config.yml` and `translations.yml`.

## License

This project is licensed under the GNU General Public License v3.0. See the [LICENSE](https://github.com/arannnn7808/IntercomDisplayInfo/blob/master/LICENSE) file for details.
