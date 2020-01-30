/*\
$$$  Project : Firebase
$$$  Author  : Modetor
$$$  Started : 29.1.2020 12:47AM (it's midnight ^_^)
$$$  
\*/

namespace Modetor.Data.Firebase
{
    public class FirebaseReference
    {
        private FirebaseReference(string appname) { ProjectUri = $"https://{appname}.firebaseio.com/"; }
        public readonly string ProjectUri;

        public DatabaseReference GetDatabaseReference()
        {
            return new DatabaseReference();
        }
        public DatabaseReference GetDatabaseReference(string StartPointNode)
        {
            return new DatabaseReference(ProjectUri+StartPointNode);
        }

        private static FirebaseReference instance = null;

        public static FirebaseReference GetReference()
        {
            if (instance == null) throw new System.NullReferenceException("FirebaseReference.GetReference(appname) must be called first to initialize the reference");
            return instance;
        }
        public static FirebaseReference GetReference(string appname)
        {
            if (instance == null) instance = new FirebaseReference(appname);
            return instance;
        }
    }
}
