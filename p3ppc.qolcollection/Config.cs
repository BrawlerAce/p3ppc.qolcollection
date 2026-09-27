using p3ppc.qolcollection.Template.Configuration;
using Reloaded.Mod.Interfaces.Structs;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace p3ppc.qolcollection.Configuration
{
    public class Config : Configurable<Config>
    {
        /*
            User Properties:
                - Please put all of your configurable properties here.
    
            By default, configuration saves as "Config.json" in mod user config folder.    
            Need more config files/classes? See Configuration.cs
    
            Available Attributes:
            - Category
            - DisplayName
            - Description
            - DefaultValue

            // Technically Supported but not Useful
            - Browsable
            - Localizable

            The `DefaultValue` attribute is used as part of the `Reset` button in Reloaded-Launcher.
        */
        public enum DescriptionEnum
        {
            [Display(Name = "Useful Descriptions")]
            UsefulDescriptions,
            [Display(Name = "Maragilao to Maragion")]
            Maragion,
            [Display(Name = "Stock P3P")]
            Stock,
        }

        [Category("Settings")]
        [DisplayName("Skill and Item Descriptions")]
        [Description("Choose which skill/item/etc descriptions to use." +
            "\n\nUseful Descriptions: Enables Useful Descriptions by yukineko. Useful Descriptions adds" +
            "\ntechnical descriptions to various skills, items, etc. For example, it includes base power,\n" +
            "accuracy, critical rates, etc. and reduces the ambiguity of item descriptions." +
            "\n\nMaragilao to Maragion: This simply changes the skill name Maragilao to Maragion to" +
            "\nbe consistent with other Persona games. Otherwise, stock P3P descriptions are used." +
            "\n\nStock P3P: Uses the default P3P skill and item descriptions.")]
        [DefaultValue(DescriptionEnum.Maragion)]
        public DescriptionEnum Description { get; set; } = DescriptionEnum.Maragion;
    }

    /// <summary>
    /// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
    /// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
    /// </summary>
    public class ConfiguratorMixin : ConfiguratorMixinBase
    {
        // 
    }
}
