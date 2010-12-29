using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OscarBrouwer.Framework;

namespace FrameworkTestApp {
  [Serializable]
  class TypeA : ICloneable {
    public TypeA() {
      this.PropI = "Hoi";
    }

    public bool PropA { get; set; }
    public bool PropB { get; set; }
    public bool PropC { get; set; }
    public bool PropD { get; set; }
    public bool PropE { get; set; }
    public bool PropF { get; set; }
    public bool PropG { get; set; }

    public string PropH { get; set; }
    public string PropI { get; private set; }
    public string PropJ { get; set; }
    public string PropK { get; set; }
    public string PropL { get; set; }
    public string PropM { get; set; }


    //public List<int> PropN { get; set; }
    //public List<DateTime> PropO { get; set; }
    //public List<TypeB> PropP { get; set; }
    //public List<TypeC> PropQ { get; set; }

    public TypeB PropR { get; set; }
    public TypeB PropS { get; set; }

    public TypeC PropT { get; set; }
    public TypeC PropU { get; set; }

    public object Clone() {
      switch(Program.Flag) {
        case 1:
          return this.CloneA();
        case 2:
          return this.CloneB();
        case 3:
          return this.CloneC();
        default:
          return this.CloneD();
      }
    }

    private object CloneA() {
      TypeA clone = new TypeA();
      clone.PropA = this.PropA;
      clone.PropB = this.PropB;
      clone.PropC = this.PropC;
      clone.PropD = this.PropD;
      clone.PropE = this.PropE;
      clone.PropF = this.PropF;
      clone.PropG = this.PropG;
      clone.PropH = this.PropH;
      clone.PropI = this.PropI;
      clone.PropJ = this.PropJ;
      clone.PropK = this.PropK;
      clone.PropL = this.PropL;
      clone.PropM = this.PropM;
      //clone.PropN = this.PropN;
      //clone.PropO = this.PropO;
      //clone.PropP = this.PropP.Select(a => a.Clone<TypeB>()).ToList();
      //clone.PropQ = this.PropQ;
      clone.PropR = this.PropR.Clone<TypeB>();
      clone.PropS = this.PropS.Clone<TypeB>();
      clone.PropT = this.PropT;
      clone.PropU = this.PropU;
      return clone;
    }

    private object CloneB() {
      TypeA clonedInstance = new TypeA();

      IEnumerable<PropertyInfo> properties =
        typeof(TypeA).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        .Where(p => p.CanWrite);

      /* Loop through the properties */
      foreach(PropertyInfo property in properties) {
        /* First, simply copy the value */
        property.SetValue(clonedInstance, property.GetValue(this, null), null);
      }

      clonedInstance.PropR = this.PropR.Clone<TypeB>();
      clonedInstance.PropS = this.PropS.Clone<TypeB>();

      return clonedInstance;
    }

    private object CloneC() {
      /* First, determine the runtime type of this instance */
      Type classType = this.GetType();

      /* Create a memberwise-clone of this instance. That will take care of cloning the value-type members */
      //object clonedInstance = this.MemberwiseClone();
      object clonedInstance = new TypeA();

      /* Get all the instance-properties that have a setter that is reacable from 'classType' point of view. */
      IEnumerable<PropertyInfo> properties =
        classType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        .Where(p => p.CanWrite);

      /* Loop through the properties */
      foreach(PropertyInfo property in properties) {
        /* First, simply copy the value */
        property.SetValue(clonedInstance, property.GetValue(this, null), null);
        
        Type[] interfaces = property.PropertyType.GetInterfaces();
        if(interfaces.Contains(typeof(ICloneable))) {
          /* The property-type implements ICloneable, so calling the Clone method will do the trick. */
          ICloneable propertyValue = (ICloneable)property.GetValue(this, null);
          if(propertyValue != null) {
            property.SetValue(clonedInstance, propertyValue.Clone(), null);
          }
        }
        else if(interfaces.Contains(typeof(IEnumerable))) {
          IEnumerable originalPropertyValue = (IEnumerable)property.GetValue(this, null);
          if(interfaces.Contains(typeof(IList))) {  
            IList propertyValue = (IList)property.GetValue(clonedInstance, null);
            int listIndex = -1;
            foreach(object obj in originalPropertyValue) {
              if(obj.GetType().GetInterfaces().Contains(typeof(ICloneable))) {
                propertyValue[++listIndex] = ((ICloneable)obj).Clone();
              }
            }
          }
          else if(interfaces.Contains(typeof(IDictionary))) {
            IDictionary propertyValue = (IDictionary)property.GetValue(clonedInstance, null);
            foreach(DictionaryEntry entry in originalPropertyValue) {
              if(entry.Value.GetType().GetInterfaces().Contains(typeof(ICloneable))) {
                propertyValue[entry.Key] = ((ICloneable)entry.Value).Clone();
              }
            }
          }
        }
      }

      return clonedInstance;
    }

    private object CloneD() {
      TypeA clone = this.MemberwiseClone() as TypeA;
      clone.PropR = this.PropR.Clone<TypeB>();
      clone.PropS = this.PropS.Clone<TypeB>();
      return clone;
    }
  }

  [Serializable]
  class TypeB : ICloneable {
    public TypeB() {
      this.PropA = 5;
    }

    public int PropA { get; private set; }
    public string PropB { get; set; }
    public object PropC { get; set; }

    public object Clone() {
      switch(Program.Flag) {
        case 1:
          return this.CloneA();
        case 2:
          return this.CloneB();
        case 3:
          return this.CloneC();
        default:
          return this.CloneD();
      }
    }

    private object CloneA() {
      TypeB clone = new TypeB();
      clone.PropA = this.PropA;
      clone.PropB = this.PropB;
      clone.PropC = this.PropC;
      return clone;
    }

    private object CloneB() {
      //TypeB clonedInstance = Activator.CreateInstance<TypeB>();
      TypeB clonedInstance = new TypeB();
      
      IEnumerable<PropertyInfo> properties =
        typeof(TypeB).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        .Where(p => p.CanWrite);

      /* Loop through the properties */
      foreach(PropertyInfo property in properties) {
        /* First, simply copy the value */
        property.SetValue(clonedInstance, property.GetValue(this, null), null);
      }

      return clonedInstance;
    }

    private object CloneC() {
      /* First, determine the runtime type of this instance */
      Type classType = this.GetType();

      /* Create a memberwise-clone of this instance. That will take care of cloning the value-type members */
      //object clonedInstance = this.MemberwiseClone();
      object clonedInstance = new TypeB();

      /* Get all the instance-properties that have a setter that is reacable from 'classType' point of view. */
      IEnumerable<PropertyInfo> properties =
        classType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        .Where(p => p.CanWrite);

      /* Loop through the properties */
      foreach(PropertyInfo property in properties) {
        /* First, simply copy the value */
        property.SetValue(clonedInstance, property.GetValue(this, null), null);

        Type[] interfaces = property.PropertyType.GetInterfaces();
        if(interfaces.Contains(typeof(ICloneable))) {
          /* The property-type implements ICloneable, so calling the Clone method will do the trick. */
          ICloneable propertyValue = (ICloneable)property.GetValue(this, null);
          property.SetValue(clonedInstance, propertyValue.Clone(), null);
        }
        else if(interfaces.Contains(typeof(IEnumerable))) {
          //IEnumerable propertyValue = (IEnumerable)property.GetValue(this, null);
          if(interfaces.Contains(typeof(IList))) {
            IList propertyValue = (IList)property.GetValue(clonedInstance, null);
            int listIndex = -1;
            foreach(object obj in propertyValue) {
              if(obj.GetType().GetInterfaces().Contains(typeof(ICloneable))) {
                propertyValue[++listIndex] = ((ICloneable)obj).Clone();
              }
            }
          }
          else if(interfaces.Contains(typeof(IDictionary))) {
            IDictionary propertyValue = (IDictionary)property.GetValue(clonedInstance, null);
            foreach(DictionaryEntry entry in propertyValue) {
              if(entry.Value.GetType().GetInterfaces().Contains(typeof(ICloneable))) {
                propertyValue[entry.Key] = ((ICloneable)entry.Value).Clone();
              }
            }
          }
        }
      }

      return clonedInstance;
    }

    private object CloneD() {
      TypeB clone = this.MemberwiseClone() as TypeB;
      return clone;
    }
  }

  [Serializable]
  class TypeC {
    public bool PropA { get; set; }
    public bool PropB { get; set; }
  }

  class Program {
    public static int Flag = 1;

    static void Main(string[] args) {
      TypeA baseA = new TypeA();
      baseA.PropA = true;
      baseA.PropB = true;
      baseA.PropC = false;
      baseA.PropD = true;
      baseA.PropE = true;
      baseA.PropF = false;
      baseA.PropG = false;

      baseA.PropH = "PropH";
      baseA.PropJ = "PropJ";
      baseA.PropK = "PropK";
      baseA.PropL = "PropL";
      baseA.PropM = "PropM";

      //baseA.PropN = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
      //baseA.PropO = new List<DateTime>(new DateTime[] {DateTime.Now, DateTime.UtcNow, DateTime.Today, DateTime.MinValue, DateTime.MaxValue });

      //baseA.PropP = new List<TypeB>(
      //  new TypeB[] { 
      //    new TypeB(){PropB="PropB Arr", PropC=new TimeoutException("TimeOut player 1!")},
      //    new TypeB(){PropB="PropB Arr", PropC=new TimeoutException("TimeOut player 2!")},
      //    new TypeB(){PropB="PropB Arr", PropC=new TimeoutException("TimeOut player 3!")},
      //    new TypeB(){PropB="PropB Arr", PropC=new TimeoutException("TimeOut player 4!")},
      //  }
      //);
      //baseA.PropQ = new List<TypeC>(
      //  new TypeC[] { 
      //    new TypeC() { PropA = true, PropB = false }, 
      //    new TypeC() { PropA = true, PropB = true },
      //    new TypeC() { PropA = false, PropB = false } 
      //  }
      //);

      baseA.PropR = new TypeB() { PropB = "TypeB.PropB", PropC = new InvalidTypeParameterException("Exception") };
      baseA.PropS = null;
      baseA.PropT = new TypeC() { PropA = true, PropB = true };
      baseA.PropU = null;

      int rounds = 1000000;

      List<TimeSpan> timings = new List<TimeSpan>();
      Program.Flag = 1;
      for(int i = 0; i < rounds; ++i) {
        DateTime roundStart = DateTime.Now;
        TypeA cloneA = baseA.Clone<TypeA>();
        DateTime roundEnd = DateTime.Now;
        timings.Add(roundEnd - roundStart);
      }

      Console.WriteLine("Testing method A (simple) finished!");
      Console.WriteLine("  Total time: {0} milliseconds (count {1})", timings.Sum(ts => ts.TotalMilliseconds), timings.Count);
      Console.WriteLine("  Mean time:  {0} milliseconds", timings.Sum(ts => ts.TotalMilliseconds) / timings.Count);
      var minEntry = timings.Select((ts, i) => new { Duration = ts.TotalMilliseconds, Index = i }).OrderBy(a=>a.Duration).First();
      Console.WriteLine("  Min time:   {0} milliseconds (entry {1})", minEntry.Duration, minEntry.Index);
      var maxEntry = timings.Select((ts, i) => new { Duration = ts.TotalMilliseconds, Index = i }).OrderByDescending(a => a.Duration).First();
      Console.WriteLine("  Max time:   {0} milliseconds (entry {1})", maxEntry.Duration, maxEntry.Index);

      timings = new List<TimeSpan>();
      Program.Flag = 2;
      for(int i = 0; i < rounds; ++i) {
        DateTime roundStart = DateTime.Now;
        TypeA cloneA = baseA.Clone<TypeA>();
        DateTime roundEnd = DateTime.Now;
        timings.Add(roundEnd - roundStart);
      }

      Console.WriteLine("Testing method B (basic reflection) finished!");
      Console.WriteLine("  Total time: {0} milliseconds (count {1})", timings.Sum(ts => ts.TotalMilliseconds), timings.Count);
      Console.WriteLine("  Mean time:  {0} milliseconds", timings.Sum(ts => ts.TotalMilliseconds) / timings.Count);
      minEntry = timings.Select((ts, i) => new { Duration = ts.TotalMilliseconds, Index = i }).OrderBy(a => a.Duration).First();
      Console.WriteLine("  Min time:   {0} milliseconds (entry {1})", minEntry.Duration, minEntry.Index);
      maxEntry = timings.Select((ts, i) => new { Duration = ts.TotalMilliseconds, Index = i }).OrderByDescending(a => a.Duration).First();
      Console.WriteLine("  Max time:   {0} milliseconds (entry {1})", maxEntry.Duration, maxEntry.Index);

      //timings = new List<TimeSpan>();
      //Program.Flag = 3;
      //for(int i = 0; i < rounds; ++i) {
      //  DateTime roundStart = DateTime.Now;
      //  TypeA cloneA = baseA.Clone<TypeA>();
      //  DateTime roundEnd = DateTime.Now;
      //  timings.Add(roundEnd - roundStart);
      //}

      //Console.WriteLine("Testing method C (extended reflection) finished!");
      //Console.WriteLine("  Total time: {0} milliseconds (count {1})", timings.Sum(ts => ts.TotalMilliseconds), timings.Count);
      //Console.WriteLine("  Mean time:  {0} milliseconds", timings.Sum(ts => ts.TotalMilliseconds) / timings.Count);
      //minEntry = timings.Select((ts, i) => new { Duration = ts.TotalMilliseconds, Index = i }).OrderBy(a => a.Duration).First();
      //Console.WriteLine("  Min time:   {0} milliseconds (entry {1})", minEntry.Duration, minEntry.Index);
      //maxEntry = timings.Select((ts, i) => new { Duration = ts.TotalMilliseconds, Index = i }).OrderByDescending(a => a.Duration).First();
      //Console.WriteLine("  Max time:   {0} milliseconds (entry {1})", maxEntry.Duration, maxEntry.Index);

      timings = new List<TimeSpan>();
      Program.Flag = 4;
      for(int i = 0; i < rounds; ++i) {
        DateTime roundStart = DateTime.Now;
        TypeA cloneA = baseA.Clone<TypeA>();
        DateTime roundEnd = DateTime.Now;
        timings.Add(roundEnd - roundStart);
      }

      Console.WriteLine("Testing method D (memberwiseclone) finished!");
      Console.WriteLine("  Total time: {0} milliseconds (count {1})", timings.Sum(ts => ts.TotalMilliseconds), timings.Count);
      Console.WriteLine("  Mean time:  {0} milliseconds", timings.Sum(ts => ts.TotalMilliseconds) / timings.Count);
      minEntry = timings.Select((ts, i) => new { Duration = ts.TotalMilliseconds, Index = i }).OrderBy(a => a.Duration).First();
      Console.WriteLine("  Min time:   {0} milliseconds (entry {1})", minEntry.Duration, minEntry.Index);
      maxEntry = timings.Select((ts, i) => new { Duration = ts.TotalMilliseconds, Index = i }).OrderByDescending(a => a.Duration).First();
      Console.WriteLine("  Max time:   {0} milliseconds (entry {1})", maxEntry.Duration, maxEntry.Index);

      Console.ReadLine();
    }
  }
}
