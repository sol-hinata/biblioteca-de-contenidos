using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BotDetect;
using BotDetect.Web;
using BotDetect.Web.UI;
using BotDetect.Web.UI.Mvc;

public class CaptchaHelper
{
    public static MvcCaptcha GetRegistrationCaptcha()
    {
        // create the control instance
        MvcCaptcha registrationCaptcha = new MvcCaptcha("RegistrationCaptcha");
        registrationCaptcha.UserInputClientID = "CaptchaCode";

        // all Captcha properties are set in this event handler
        registrationCaptcha.InitializedCaptchaControl +=
            new EventHandler<InitializedCaptchaControlEventArgs>(
                RegistrationCaptcha_InitializedCaptchaControl);

        // all Captcha settings have to be saved before rendering
        registrationCaptcha.SaveSettings();

        return registrationCaptcha;
    }

    // event handler used for Captcha control property randomization
    static void RegistrationCaptcha_InitializedCaptchaControl(object sender, 
        InitializedCaptchaControlEventArgs e)
    {
        if (e.CaptchaId != "RegistrationCaptcha")
        {
            return;
        }

        CaptchaControl registrationCaptcha = sender as CaptchaControl;

        // fixed Captcha settings 
        registrationCaptcha.ImageSize = new System.Drawing.Size(200, 50);
        registrationCaptcha.CodeLength = 4;

        // randomized Captcha settings
        registrationCaptcha.ImageStyle = CaptchaRandomization.GetRandomImageStyle();
        registrationCaptcha.SoundStyle = CaptchaRandomization.GetRandomSoundStyle();
    }
}
