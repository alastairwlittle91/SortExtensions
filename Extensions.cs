namespace SortExtensions;
public static class Extensions
{
    public static IList<int> BubbleSort(this IList<int> list)
    {
        var total = list.Count();
        var hasMoved = false;

        for (var i = 0; i < total - 1; i ++)
            if (list[i] > list[i + 1])
            {
                list.Swap(i, i + 1);
                hasMoved = true;
            }

        if (hasMoved)
            BubbleSort(list);

        return list;
    }

    public static IList<int> MergeSort(this IList<int> list)
    {
        var results = Enumerable.Empty<int>();

        if (list.Count() > 1)
        {
            var middle = list.Count() / 2;
            var left = list.Take(middle).ToList();
            var right = list.Skip(middle).Take(list.Count() % 2 != 0 ? middle + 1 : middle).ToList();

            left = left.MergeSort().ToList();
            right = right.MergeSort().ToList();

            results = Sort(left,right);
        }
        else 
        {
            results = list;
        }

        return results.ToList();
    }

    private static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
    {
        T tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
        return list;
    }

    private static IList<int> Sort(IList<int> left, IList<int> right) 
    {
        var output = Enumerable.Empty<int>();
        var trackLeft = 1;
        var trackRight = 1;

        while (trackLeft <= left.Count() || trackRight <= right.Count())
        {

            if (trackLeft <= left.Count() && trackRight <= right.Count())
            {
                var leftTarget = trackLeft switch {
                    1 => left.First(),
                    _ => left.Skip(trackLeft -1).First()
                };

                var rightTarget = trackRight switch {
                    1 => right.First(),
                    _ => right.Skip(trackRight -1).First()
                };

                output = output.Append(leftTarget <= rightTarget ? leftTarget : rightTarget);
                
                trackLeft = leftTarget <= rightTarget ? trackLeft + 1 : trackLeft;
                trackRight = rightTarget <= leftTarget ? trackRight + 1 : trackRight;
            
            }
            else 
            {
                if(trackLeft <= left.Count())
                {
                    output = output.Concat(left.Skip(trackLeft -1));
                    trackLeft = left.Count() + 1;
                }
                else if (trackRight <= right.Count())
                {
                    output = output.Concat(right.Skip(trackRight -1));
                    trackRight = right.Count() + 1;
                }
            }
        }

        return output.ToList();
    }
}