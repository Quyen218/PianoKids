public static class NoteDefine
{
    public static ENoteDef ConvertNote(string note)
    {
        switch (note)
        {
            case "do":
            case "c":
                return ENoteDef.DO;
            case "re":
            case "d":
                return ENoteDef.RE;
            case "mi":
            case "e":
                return ENoteDef.MI;
            case "fa":
            case "f":
                return ENoteDef.FA;
            case "so":
            case "sol":
            case "g":
                return ENoteDef.SOL;
            case "la":
            case "a":
                return ENoteDef.LA;
            case "si":
            case "ti":
            case "b":
                return ENoteDef.TI;
            case "d2":
            case "do#":
            case "c#": //don't know it is c-sharp or c-flat
                return ENoteDef.DO2;

            default:
                return ENoteDef.Break;
        }
    }
}

public enum ENoteDef
{
    DO,
    RE,
    MI,
    FA,
    SOL,
    LA,
    TI,
    DO2,
    Break, //it is not a note
}