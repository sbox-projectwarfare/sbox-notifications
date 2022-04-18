# <b>Notifications Library for S&amp;box Project Warfare</b>
## This is a library that allows you to call notification UI panels

<p align="center"> <img src="readme_img/notifications_examples.png"/></p>

## <b>How to add library to your addon?</b>
Clone this repository to folder you need via `git clone` command or add as submodule via [`git submodule`](https://git-scm.com/book/en/v2/Git-Tools-Submodules) command.

## <b>How to use it?</b>
This repository has a main part with library's source code and an example project, that you can use to understand how this library can be used. Keep in mind, in example project backend part stores in `code/notifications` and frontend in `ui/notifications`

To activate library you must:
* Initialize `NotificationStack` in game script's constructor to store data;
![Manager init](readme_img/manager_init.png)
* Call `NotificationStack.Push` method from it in player's code (check "Notifications Library API") with data type you need;
![Calling method](readme_img/manager_shownotification.png)
* Have fun.

## <b>How to make a custom notification?</b>
If you want to make your own notification type with custom style, you need to do:
* Inherit your custom class from `Notification`;

![Custom notification example](readme_img/custom_notification.png)

* Open `styles/NotificationsStyle.scss` file and set style for your custom notification type. For example, since we wrote "yellow" in class, we should add `&.yellow` class style realization. If you have any problems, check styles code above.

![Style example](readme_img/style_example.png)
* Now you can call `NotificationStack.Push` method with your new type.

![Custom method example](readme_img/custom_method.png)
![Custom notification example](readme_img/custom_notification_result.png)

## <b>We'll be glad if you send your feedback about library and what problems you've get to ["Issues"](https://github.com/sbox-MillitaryRP/sbox-mrp-notifications/issues) section</b>
