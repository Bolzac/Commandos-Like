public class Ladder : Movable
{
    //Merdivenleri orijin noktaları tam ortasında. Karakter oraya gidemeyeceği için move'un hemen ardından Interaction çalışır.
    //Interaction içerisinde ise hangi noktaya gideceği belirlenir.
    public override void Interaction(Member member)
    {
        member.controller.Move(transform.position.y < member.transform.position.y
            ? offMeshLink.startTransform.position
            : offMeshLink.endTransform.position);
    }
}
