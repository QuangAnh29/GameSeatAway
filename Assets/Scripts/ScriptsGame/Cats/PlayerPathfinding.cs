using System.Collections.Generic;
using Sirenix.OdinInspector;

public class PlayerPathfinding
{
    private HoldingBase _start;
    private PlayerCtrl _playerCtrl;
    private GameController _gameController;

    public PlayerPathfinding(HoldingBase start, PlayerCtrl playerCtrl, GameController gameController)
    {
        _start = start;
        _playerCtrl = playerCtrl;
        _gameController = gameController;
    }

    [Title("Kiem tra o vi tri xuat phat co ghe nam tren khong")]
    public ChairCtrl GetChairAtStart()
    {
        foreach (var chair in _gameController.chairCtrls)
        {
            if (chair.HoldingBaseUsing == _start)
            {
                return chair;
            }
        }
        return null;
    }

    [Button]
    [Title("6 - Duong di ngan nhat")]
    public List<HoldingBase> FindShortestPathToChairSameColor()
    {
        var newHoldingBase = HoldingBaseOfChairSameColorAndShortestPath();
        if (newHoldingBase == null) return null;
        var path = MazeSolver.instance.FindPath(_start, newHoldingBase);
        return path;
    }

    [Button]
    [Title("5 - tra ve ten ghe co the ngoi")]
    public ChairCtrl CheckCanSit()
    {
        var targetHoldingBase = HoldingBaseOfChairSameColorAndShortestPath();
        if (targetHoldingBase == null) return null;
        var listChairSameColorCanMove = ListChairSameColorCanMove();

        foreach (var chair in listChairSameColorCanMove)
        {
            foreach (var holding in chair.DisplayHoldingBaseCanMove())
            {
                if (holding == targetHoldingBase)
                {
                    return chair;
                }
            }
        }
        return null;
    }

    [Title("")]
    public bool CanReachChairSameColor()
    {
        var targetHoldingBase = HoldingBaseOfChairSameColorAndShortestPath();
        if (targetHoldingBase == null) return false;
        var path = MazeSolver.instance.FindPath(_start, targetHoldingBase);
        return path != null;
    }

    [Title("4 - Hoding Base gan ghe co vi tri gan voi player, cung mau, co duong di den ghe do")]
    public HoldingBase HoldingBaseOfChairSameColorAndShortestPath()
    {
        var sameColorHoldingBasesCanMove = ListHoldingBaseAdjacentChairSameColor();
        HoldingBase holdingBase = null;

        List<HoldingBase> shortestPath = null;
        var shortestDistance = int.MaxValue;
        foreach (var newhB in sameColorHoldingBasesCanMove)
        {
            var path = MazeSolver.instance.FindPath(_start, newhB);

            if (path != null && path.Count < shortestDistance && path.Count > 0)
            {
                shortestPath = path;
                shortestDistance = path.Count;
                holdingBase = newhB;
            }
        }
        return holdingBase;
    }

    [Title("3 - List Holding Base gan ghe cung mau va co the di chuyen duoc")]
    private List<HoldingBase> ListHoldingBaseAdjacentChairSameColor()
    {
        var listChairSameColorCanMove = ListChairSameColorCanMove();
        var listHoldingBaseCanMove = new List<HoldingBase>();

        var uniqueHoldingBases = new HashSet<HoldingBase>();

        foreach (var chair in listChairSameColorCanMove)
        {
            foreach (var holdingBase in chair.DisplayHoldingBaseCanMove())
            {
                if (!uniqueHoldingBases.Contains(holdingBase))
                {
                    listHoldingBaseCanMove.Add(holdingBase);
                    uniqueHoldingBases.Add(holdingBase);
                }
            }
        }
        return listHoldingBaseCanMove;
    }

    [Title("2 - List ghe cung mau va co the di chuyen den")]
    private List<ChairCtrl> ListChairSameColorCanMove()
    {
        var sameColorHoldingBases = GetChairSameColor();
        var chairSameColorCanMove = new List<ChairCtrl>();
        foreach (var chair in sameColorHoldingBases)
        {
            if (chair.CheckPositionCanMove())
            {
                chairSameColorCanMove.Add(chair);
            }
        }
        return chairSameColorCanMove;
    }

    [Title("1 - List ghe cung mau voi nhan vat")]
    private List<ChairCtrl> GetChairSameColor()
    {
        var sameColorHoldingBases = new List<ChairCtrl>();
        var playerColor = _playerCtrl.GetColor();

        foreach (var chair in _gameController.chairCtrls)
        {
            if (!PlayerMovement.occupiedChairs.Contains(chair))
            {
                if (chair.CanSit(playerColor) && !chair.IsPlayerSit)
                {
                    sameColorHoldingBases.Add(chair);
                }
            }
        }
        return sameColorHoldingBases;
    }
}
